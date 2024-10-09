using NexusGate.Configurations;
using NexusGate.Infrastructure.Configurations;

namespace NexusGate;

public static class ConfigurationBootstrap
{
    public static IServiceCollection AddConfigurationsBootstrap(this IServiceCollection services)
    {
        // Endpoint Configurations
        services.AddEndpointConfiguration();
        services.AddApiDocumentationConfiguration();
        services.AddModulesConfiguration();

        // Service Configurations
        services.AddBackgroundJobConfiguration();
        services.AddGlobalExceptionConfiguration();
        services.AddMediatorConfiguration();
        services.AddFluentValidationConfiguration();

        // Infrastructure Configurations
        services.AddRateLimiterConfiguration();
        services.AddAuthenticationConfiguration();
        services.AddLoggerConfiguration();
        services.AddHealthCheckConfiguration();
        services.AddCorsConfiguration();
        return services;
    }

    public static WebApplication UseConfigurationsBootstrap(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseApiDocumentationConfiguration();
        }

        app.UseCorsConfiguration();
        app.UseLoggerConfiguration();
        app.UseRateLimiterConfiguration();
        app.UseGlobalExceptionConfiguration();
        app.UseHealthCheckConfiguration();
        
        // UseEndpointConfiguration ideally should be at the most bottom
        app.UseEndpointConfiguration();
        return app;
    }
}