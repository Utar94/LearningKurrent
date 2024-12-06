using LearningKurrent.Application.Products;
using LearningKurrent.Infrastructure.Queriers;
using LearningKurrent.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LearningKurrent.Infrastructure;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddLearningKurrentInfrastructure(this IServiceCollection services)
  {
    return services
      .AddScoped<IProductQuerier, ProductQuerier>()
      .AddScoped<IProductRepository, ProductRepository>();
  }
}
