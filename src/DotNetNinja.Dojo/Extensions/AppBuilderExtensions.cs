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
            endpoints.MapControllers();
        });
    }

    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (!environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
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