namespace LearningKurrent.Domain;

public interface IDeleteControlEvent : IEvent
{
  bool? IsDeleted { get; }
}
