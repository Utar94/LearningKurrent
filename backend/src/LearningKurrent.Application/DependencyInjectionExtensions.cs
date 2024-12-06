using Microsoft.Extensions.DependencyInjection;

namespace LearningKurrent.Application;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLearningKurrentApplication(this IServiceCollection services)
  {
    return services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
  }
}
