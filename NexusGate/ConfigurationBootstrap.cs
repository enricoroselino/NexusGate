using System.Reflection;
using NexusGate.Configurations;
using NexusGate.Infrastructure.Configurations;

namespace NexusGate;

public static class ConfigurationBootstrap
{
    public static IServiceCollection AddConfigurationsBootstrap(this IServiceCollection services, Assembly assembly)
    {
        // Endpoint Configurations
        services.AddEndpointConfiguration();
        services.AddApiDocumentationConfiguration();
        services.AddModulesDiscoveryConfiguration();

        // Service Configurations
        services.AddFluentValidationConfiguration(assembly);
        services.AddBackgroundJobConfiguration();
        services.AddMediatorConfiguration(assembly);
        services.AddGlobalExceptionConfiguration();

        // Infrastructure Configurations
        services.AddRateLimiterConfiguration();
        services.AddJsonWebTokenConfiguration();
        services.AddLoggerConfiguration();
        return services;
    }

    public static WebApplication UseConfigurationsBootstrap(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseApiDocumentationConfiguration();
        }
        
        app.UseLoggerConfiguration();
        app.UseRateLimiterConfiguration();
        app.UseGlobalExceptionConfiguration();
        
        // UseEndpointConfiguration ideally should be at the most bottom
        app.UseEndpointConfiguration();
        return app;
    }
}