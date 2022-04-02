using System.Reflection;

using DotNetNinja.AutoBoundConfiguration;
using DotNetNinja.Dojo.Configuration;
using DotNetNinja.Dojo.Entities.Migrations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

namespace DotNetNinja.Dojo.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
                .AddScoped<IDbMigrator, SqlDbMigrator>();
    }

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

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

    public static IServiceCollection AddDataContext<TContext>(this IServiceCollection services,
        IAutoBoundConfigurationProvider provider) where TContext : DbContext
    {
        return services.AddDataContext<TContext>(provider, typeof(TContext).Name);
    }

    public static IServiceCollection AddDataContext<TContext>(this IServiceCollection services,
        IAutoBoundConfigurationProvider provider, string connectionName) where TContext : DbContext
    {
        var settings = provider.Get<DbSettings>();
        var config = settings.Contexts[connectionName];
        return services.AddDbContext<TContext>(options => options.UseSqlServer(config.ConnectionString));
    }

    public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services, 
        IAutoBoundConfigurationProvider provider)
    {
        // Add Sql Checks
        var settings = provider.Get<DbSettings>();
        var builder = services.AddHealthChecks();
        foreach (var key in settings.Contexts.Keys)
        {
            var db = settings.Contexts[key];
            if (db.Type == DbType.SqlServer)
            {
                builder.AddSqlServer(db.ConnectionString, 
                    "SELECT 1", 
                    db.Name, 
                    HealthStatus.Unhealthy, 
                    new[]
                    {
                        "Database",
                        "SqlServer"
                    }, 
                    TimeSpan.FromSeconds(3));
            }
        }

        return builder.Services;
    }
}