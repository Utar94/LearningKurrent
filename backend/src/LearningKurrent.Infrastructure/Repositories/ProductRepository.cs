using EventStore.Client;
using LearningKurrent.Application.Products;
using LearningKurrent.Domain;
using LearningKurrent.Domain.Products;
using LearningKurrent.Infrastructure.Converters;
using Logitar;
using MediatR;

namespace LearningKurrent.Infrastructure.Repositories;

internal class ProductRepository : IProductRepository
{
  private static readonly JsonSerializerOptions _serializerOptions = new();
  static ProductRepository()
  {
    _serializerOptions.Converters.Add(new ActorIdConverter());
    _serializerOptions.Converters.Add(new AggregateIdConverter());
    _serializerOptions.Converters.Add(new DescriptionConverter());
    _serializerOptions.Converters.Add(new DisplayNameConverter());
    _serializerOptions.Converters.Add(new EventIdConverter());
    _serializerOptions.Converters.Add(new PriceConverter());
    _serializerOptions.Converters.Add(new SkuConverter());
    _serializerOptions.Converters.Add(new UrlConverter());
  }

  private readonly EventStoreClient _client;
  private readonly IPublisher _publisher;

  public ProductRepository(EventStoreClient client, IPublisher publisher)
  {
    _client = client;
    _publisher = publisher;
  }

  public async Task<Product?> LoadAsync(ProductId id, CancellationToken cancellationToken)
  {
    return await LoadAsync(id, version: null, cancellationToken);
  }
  public async Task<Product?> LoadAsync(ProductId id, long? version, CancellationToken cancellationToken)
  {
    EventStoreClient.ReadStreamResult events = _client.ReadStreamAsync(
      direction: Direction.Forwards,
      streamName: id.Value,
      revision: version.HasValue ? StreamPosition.FromInt64(version.Value) : StreamPosition.Start,
      cancellationToken: cancellationToken);

    if (await events.ReadState == ReadState.StreamNotFound)
    {
      return null;
    }

    List<IEvent> domainEvents = [];
    await foreach (ResolvedEvent @event in events)
    {
      Type eventType = Type.GetType(@event.Event.EventType)
        ?? throw new InvalidOperationException($"The type '{@event.Event.EventType}' could not be resolved.");

      string json = Encoding.UTF8.GetString(@event.Event.Data.ToArray());
      IEvent domainEvent = JsonSerializer.Deserialize(json, eventType, _serializerOptions) as IEvent
        ?? throw new InvalidOperationException($"The specified event could not be deserialized.{Environment.NewLine}JSON: {json}");
      domainEvents.Add(domainEvent);
    }

    return AggregateRoot.LoadFromChanges<Product>(id.AggregateId, domainEvents);
  }

  public async Task SaveAsync(Product product, CancellationToken cancellationToken)
  {
    if (product.HasChanges)
    {
      await _client.AppendToStreamAsync(
        streamName: product.Id.Value,
        expectedRevision: StreamRevision.FromInt64(product.Version - product.Changes.Count - 1),
        eventData: product.Changes.Select(ToEventData),
        cancellationToken: cancellationToken);

      foreach (IEvent change in product.Changes)
      {
        await _publisher.Publish(change, cancellationToken);
      }

      product.ClearChanges();
    }
  }
  public async Task SaveAsync(IEnumerable<Product> products, CancellationToken cancellationToken)
  {
    foreach (Product product in products)
    {
      await SaveAsync(product, cancellationToken);
    }
  }

  private EventData ToEventData(IEvent @event)
  {
    Uuid eventId = Uuid.NewUuid();
    if (@event is DomainEvent domainEvent)
    {
      eventId = Uuid.FromGuid(domainEvent.Id.ToGuid());
    }
    else
    {
      if (@event is IIdentifiableEvent identifiable)
      {
        eventId = Uuid.FromGuid(identifiable.Id.ToGuid());
      }
    }

    string type = @event.GetType().GetNamespaceQualifiedName();
    string json = JsonSerializer.Serialize(@event, @event.GetType(), _serializerOptions);
    byte[] data = Encoding.UTF8.GetBytes(json);

    return new EventData(eventId, type, data);
  }
}
