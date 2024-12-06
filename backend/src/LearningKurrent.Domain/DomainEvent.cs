namespace LearningKurrent.Domain;

public abstract record DomainEvent : IActorEvent, IDeleteControlEvent, IIdentifiableEvent, ITemporalEvent
{
  public EventId Id { get; set; } = EventId.NewId();

  public AggregateId AggregateId { get; set; }
  public long Version { get; set; }

  public ActorId? ActorId { get; set; }
  public DateTime OccurredOn { get; set; } = DateTime.Now;

  public bool? IsDeleted { get; set; }
}
