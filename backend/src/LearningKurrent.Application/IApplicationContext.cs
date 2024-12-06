using LearningKurrent.Domain;

namespace LearningKurrent.Application;

public interface IApplicationContext
{
  ActorId? ActorId { get; }
}
