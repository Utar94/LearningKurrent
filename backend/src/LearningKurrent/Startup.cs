using LearningKurrent.Application;
using LearningKurrent.Infrastructure;
using Scalar.AspNetCore;

namespace LearningKurrent;

internal class Startup : StartupBase
{
  private readonly bool _enableOpenApi;

  public Startup(IConfiguration configuration)
  {
    _enableOpenApi = configuration.GetValue<bool>("EnableOpenApi");
  }

  public override void ConfigureServices(IServiceCollection services)
  {
    base.ConfigureServices(services);

    services.AddControllers();

    if (_enableOpenApi)
    {
      services.AddOpenApi();
    }

    services.AddLearningKurrentApplication();
    services.AddLearningKurrentInfrastructure();
    services.AddSingleton<IApplicationContext, HttpApplicationContext>();
  }

  public override void Configure(IApplicationBuilder builder)
  {
    if (builder is WebApplication application)
    {
      Configure(application);
    }
  }
  private void Configure(WebApplication application)
  {
    if (_enableOpenApi)
    {
      application.MapOpenApi();
      application.MapScalarApiReference();
    }

    application.UseHttpsRedirection();

    application.MapControllers();
  }
}
