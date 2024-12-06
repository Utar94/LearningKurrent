namespace LearningKurrent.Domain;

public interface IIdentifiableEvent : IEvent
{
  EventId Id { get; }
}
