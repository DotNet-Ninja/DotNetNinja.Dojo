using System.Net;

using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DotNetNinja.Dojo.Extensions;

public static class AppBuilderExtensions
{
    public static IApplicationBuilder UseHsts(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
        {
            app.UseHsts();
        }
        return app;
    }

    public static IApplicationBuilder UseApplicationEndpoints(this IApplicationBuilder app)
    {
        return app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/Liveliness", new HealthCheckOptions
            {
                Predicate = _ => false,
                ResultStatusCodes = new Dictionary<HealthStatus, int>
                {
                    {
                        HealthStatus.Degraded, (int)HttpStatusCode.OK
                    },
                    {
                        HealthStatus.Healthy, (int)HttpStatusCode.OK
                    },
                    {
                        HealthStatus.Unhealthy, (int)HttpStatusCode.ServiceUnavailable
                    },
                }
            });
            endpoints.MapHealthChecks("/Health/Databases", new HealthCheckOptions
            {
                Predicate = (item) => item.Tags.Contains("Database"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            endpoints.MapHealthChecks("/Health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            endpoints.MapControllers();
        });
    }

    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (!environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "DotNet Ninja Dojo API";
                options.InjectStylesheet("/Assets/css/Custom.Swagger.css");
            });
        }

        app.UseReDoc(options =>
        {
            options.DocumentTitle = "Dojo API";
            options.SpecUrl = "/swagger/v1/swagger.json";
            options.RoutePrefix = "docs";
        });

        return app;
    }
}