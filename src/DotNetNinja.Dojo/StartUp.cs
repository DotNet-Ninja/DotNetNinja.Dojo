using DotNetNinja.Dojo.Entities;
using DotNetNinja.Dojo.Entities.Migrations;
using DotNetNinja.Dojo.Extensions;
using DotNetNinja.Dojo.Models;

namespace DotNetNinja.Dojo;

public class StartUp
{
    public StartUp(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services
                .AddAutoBoundConfiguration(Configuration, out var bound)
                .AddAutoMapper(typeof(Entity).Assembly)
                .AddDataContext<DojoContext>(bound)
                .AddApplicationServices()
                .AddApplicationHealthChecks(bound)
                .AddControllers()
                .AddApiContentFormatters()
                .AddEndpointsApiExplorer()
                .AddOpenApiGeneration();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbMigrator migrations)
    {
        app
            .UseHsts()
            .UseOpenApi(env)
            .UseHttpsRedirection()
            .UseStaticFiles()
            .UseRouting()
            .UseAuthorization()
            .UseApplicationEndpoints();

        migrations.Migrate().SeedDatabase();
    }
}