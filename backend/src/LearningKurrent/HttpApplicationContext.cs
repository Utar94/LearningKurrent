using LearningKurrent.Application;
using LearningKurrent.Domain;

namespace LearningKurrent;

internal class HttpApplicationContext : IApplicationContext
{
  public ActorId? ActorId { get; } = new("fpion");
}
