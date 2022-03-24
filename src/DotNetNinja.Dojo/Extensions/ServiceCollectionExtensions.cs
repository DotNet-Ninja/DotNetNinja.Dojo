using DotNetNinja.AutoBoundConfiguration;

using Microsoft.OpenApi.Models;

namespace DotNetNinja.Dojo.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoBoundConfiguration(this IServiceCollection services,
        IConfiguration configuration,
        out IAutoBoundConfigurationProvider provider)
    {
        provider = services.AddAutoBoundConfigurations(configuration)
                            .FromAssembly(typeof(Program).Assembly)
                            .Provider;
        return services;
    }

    public static IServiceCollection AddOpenApiGeneration(this IServiceCollection services)
    {
        return services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "DotNet Ninja Dojo API",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = ".Net Ninja",
                    Url = new Uri("https://dotnetninja.net")
                },
                Description = "",
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://github.com/DotNet-Ninja/DotNetNinja.Dojo/blob/main/License.txt")
                },
                TermsOfService = new Uri("https://github.com/DotNet-Ninja/DotNetNinja.Dojo/")
            });
        });
    }
}