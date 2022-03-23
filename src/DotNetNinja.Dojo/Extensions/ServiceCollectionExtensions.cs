using DotNetNinja.AutoBoundConfiguration;

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
}