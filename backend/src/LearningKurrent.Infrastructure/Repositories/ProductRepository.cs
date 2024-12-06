using EventStore.Client;
using LearningKurrent.Application.Products;
using LearningKurrent.Domain;
using LearningKurrent.Domain.Products;
using LearningKurrent.Infrastructure.Converters;
using Logitar;

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

  public ProductRepository(EventStoreClient client)
  {
    _client = client;
  }

  public async Task<Product?> LoadAsync(ProductId id, CancellationToken cancellationToken)
  {
    return await LoadAsync(id, version: null, cancellationToken);
  }
  public Task<Product?> LoadAsync(ProductId id, long? version, CancellationToken cancellationToken)
  {
    throw new NotImplementedException(); // TODO(fpion): implement
  }

  public async Task SaveAsync(Product product, CancellationToken cancellationToken)
  {
    if (product.HasChanges)
    {
      await _client.AppendToStreamAsync(
        streamName: product.Id.Value,
        expectedRevision: StreamRevision.FromInt64(product.Version),
        eventData: product.Changes.Select(ToEventData),
        cancellationToken: cancellationToken);

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
