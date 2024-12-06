using EventStore.Client;
using LearningKurrent.Application.Products;
using LearningKurrent.Infrastructure.Queriers;
using LearningKurrent.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningKurrent.Infrastructure;

public static class DependencyInjectionExtensions
{
  private const string EventStoreKey = "ESDBCONNSTR_LearningKurrent";
  private const string SqlServerKey = "SQLCONNSTR_LearningKurrent";

  public static IServiceCollection AddLearningKurrentInfrastructure(this IServiceCollection services)
  {
    return services
      .AddDbContext<CommerceContext>((serviceProvider, options) =>
      {
        string connectionString = serviceProvider.GetConnectionString(SqlServerKey);
        options.UseSqlServer(connectionString, options => options.MigrationsAssembly("LearningKurrent.Infrastructure"));
      })
      .AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
      .AddScoped(serviceProvider =>
      {
        string connectionString = serviceProvider.GetConnectionString(EventStoreKey);
        EventStoreClientSettings settings = EventStoreClientSettings.Create(connectionString);
        return new EventStoreClient(settings);
      })
      .AddScoped<IProductQuerier, ProductQuerier>()
      .AddScoped<IProductRepository, ProductRepository>();
  }

  private static string GetConnectionString(this IServiceProvider serviceProvider, string key)
  {
    string? connectionString = Environment.GetEnvironmentVariable(key);
    if (string.IsNullOrWhiteSpace(connectionString))
    {
      IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
      connectionString = configuration.GetValue<string>(key);
    }
    if (string.IsNullOrWhiteSpace(connectionString))
    {
      throw new InvalidOperationException($"The configuration '{key}' could not be found.");
    }
    return connectionString;
  }
}
