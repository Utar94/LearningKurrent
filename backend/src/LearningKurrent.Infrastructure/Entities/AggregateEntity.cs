using LearningKurrent.Domain;
using Logitar;

namespace LearningKurrent.Infrastructure.Entities;

internal abstract class AggregateEntity
{
  public string AggregateId { get; private set; } = string.Empty;
  public long Version { get; private set; }

  public string? CreatedBy { get; private set; }
  public DateTime CreatedOn { get; private set; }

  public string? UpdatedBy { get; private set; }
  public DateTime UpdatedOn { get; private set; }

  protected AggregateEntity()
  {
  }

  protected AggregateEntity(AggregateId aggregateId, IEvent @event)
  {
    AggregateId = aggregateId.Value;

    if (@event is DomainEvent domainEvent)
    {
      CreatedBy = domainEvent.ActorId?.Value;
      CreatedOn = domainEvent.OccurredOn.AsUniversalTime();
    }
    else
    {
      CreatedBy = @event is IActorEvent actor ? actor.ActorId?.Value : null;
      CreatedOn = (@event is ITemporalEvent temporal ? temporal.OccurredOn : DateTime.Now).AsUniversalTime();
    }

    Update(@event);
  }

  public virtual IEnumerable<ActorId> GetActorIds()
  {
    List<ActorId> actorIds = new(capacity: 2);
    if (CreatedBy != null)
    {
      actorIds.Add(new ActorId(CreatedBy));
    }
    if (UpdatedBy != null)
    {
      actorIds.Add(new ActorId(UpdatedBy));
    }
    return actorIds;
  }

  protected void Update(IEvent @event)
  {
    if (@event is DomainEvent domainEvent)
    {
      Version = domainEvent.Version;

      UpdatedBy = domainEvent.ActorId?.Value;
      UpdatedOn = domainEvent.OccurredOn.AsUniversalTime();
    }
    else
    {
      Version++;

      UpdatedBy = @event is IActorEvent actor ? actor.ActorId?.Value : null;
      UpdatedOn = (@event is ITemporalEvent temporal ? temporal.OccurredOn : DateTime.Now).AsUniversalTime();
    }
  }
}
