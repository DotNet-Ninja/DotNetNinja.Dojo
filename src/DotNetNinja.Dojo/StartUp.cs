using DotNetNinja.AutoBoundConfiguration;
using DotNetNinja.Dojo.Extensions;

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
                .AddControllers()
                .AddApiContentFormatters()
                .AddEndpointsApiExplorer()
                .AddOpenApiGeneration();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app
            .UseHsts()
            .UseOpenApi(env)
            .UseHttpsRedirection()
            .UseStaticFiles()
            .UseRouting()
            .UseAuthorization()
            .UseApplicationEndpoints();
    }
}