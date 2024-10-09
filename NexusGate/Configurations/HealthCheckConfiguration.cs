namespace NexusGate.Configurations;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddHealthCheckConfiguration(this IServiceCollection services)
    {
        services.AddHealthChecks();
        services.AddHealthChecksUI()
            .AddInMemoryStorage();
        return services;
    }

    public static IApplicationBuilder UseHealthCheckConfiguration(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/hc");
        app.UseHealthChecksUI(cfg => { cfg.UIPath = "/hcui"; });
        return app;
    }
}