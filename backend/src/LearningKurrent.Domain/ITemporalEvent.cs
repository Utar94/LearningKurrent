namespace LearningKurrent.Domain;

public interface ITemporalEvent : IEvent
{
  DateTime OccurredOn { get; }
}
