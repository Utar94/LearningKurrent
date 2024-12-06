namespace LearningKurrent.Domain;

public abstract class AggregateRoot : IAggregate
{
  public AggregateId Id { get; private set; }
  public long Version { get; private set; }

  public ActorId? CreatedBy { get; private set; }
  public DateTime CreatedOn { get; private set; }

  public ActorId? UpdatedBy { get; private set; }
  public DateTime UpdatedOn { get; private set; }

  public bool IsDeleted { get; private set; }

  private readonly List<IEvent> _changes = [];
  public bool HasChanges => _changes.Count > 0;
  public IReadOnlyCollection<IEvent> Changes => _changes.AsReadOnly();
  public void ClearChanges() => _changes.Clear();

  protected AggregateRoot(AggregateId? id = null)
  {
    if (id.HasValue)
    {
      if (string.IsNullOrWhiteSpace(id.Value.Value))
      {
        throw new ArgumentException("TODO", nameof(id));
      }
      Id = id.Value;
    }
    else
    {
      Id = AggregateId.NewId();
    }
  }

  public static T LoadFromChanges<T>(AggregateId id, IEnumerable<IEvent> events) where T : AggregateRoot, new()
  {
    T aggregate = new()
    {
      Id = id
    };

    foreach (IEvent @event in events)
    {
      aggregate.Apply(@event);
    }

    return aggregate;
  }

  protected void Raise(IEvent @event, ActorId? actorId = null, DateTime? occurredOn = null)
  {
    if (@event is DomainEvent domainEvent)
    {
      domainEvent.AggregateId = Id;
      domainEvent.Version = Version + 1;

      if (actorId.HasValue)
      {
        domainEvent.ActorId = actorId.Value;
      }
      if (occurredOn.HasValue)
      {
        domainEvent.OccurredOn = occurredOn.Value;
      }
    }

    Apply(@event);

    _changes.Add(@event);
  }

  protected void Apply(IEvent @event)
  {
    Dispatch(@event);

    if (@event is DomainEvent domainEvent)
    {
      Version = domainEvent.Version;

      if (Version <= 1)
      {
        CreatedBy = domainEvent.ActorId;
        CreatedOn = domainEvent.OccurredOn;
      }

      UpdatedBy = domainEvent.ActorId;
      UpdatedOn = domainEvent.OccurredOn;

      if (domainEvent.IsDeleted.HasValue)
      {
        IsDeleted = domainEvent.IsDeleted.Value;
      }
    }
    else
    {
      Version++;

      ActorId? actorId = @event is IActorEvent actor ? actor.ActorId : null;
      DateTime occurredOn = @event is ITemporalEvent temporal ? temporal.OccurredOn : DateTime.Now;

      if (Version <= 1)
      {
        CreatedBy = actorId;
        CreatedOn = occurredOn;
      }

      UpdatedBy = actorId;
      UpdatedOn = occurredOn;

      List<bool> isDeleted = new(capacity: 3);
      if (@event is IDeleteEvent)
      {
        isDeleted.Add(true);
      }
      if (@event is IUndeleteEvent)
      {
        isDeleted.Add(false);
      }
      if (@event is IDeleteControlEvent deleteControl && deleteControl.IsDeleted.HasValue)
      {
        isDeleted.Add(deleteControl.IsDeleted.Value);
      }
      if (isDeleted.Count == 1)
      {
        IsDeleted = isDeleted.Single();
      }
    }
  }

  protected virtual void Dispatch(IEvent @event)
  {
    MethodInfo? handle = GetType().GetMethod("Handle", BindingFlags.Instance | BindingFlags.NonPublic, [@event.GetType()]);
    handle?.Invoke(this, [@event]);
  }

  public override bool Equals(object? obj) => obj is AggregateRoot aggregate && aggregate.GetType().Equals(GetType()) && aggregate.Id == Id;
  public override int GetHashCode() => HashCode.Combine(GetType(), Id);
  public override string ToString() => $"{GetType()} (Id={Id})";
}
