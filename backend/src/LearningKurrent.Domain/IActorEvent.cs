namespace LearningKurrent.Domain;

public interface IActorEvent : IEvent
{
  ActorId? ActorId { get; }
}
