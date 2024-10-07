using Asp.Versioning;
using Carter;
using NexusGate.Infrastructure.Configurations;

namespace NexusGate.Configurations;

public static class EndpointConfiguration
{
    public static IServiceCollection AddEndpointConfiguration(this IServiceCollection services)
    {
        services.AddEndpointVersioningConfiguration();
        services.AddCarter();
        return services;
    }

    public static IEndpointRouteBuilder UseEndpointConfiguration(this IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        app.MapGroup("api/v{apiVersion:apiVersion}")
            .WithApiVersionSet(versionSet)
            .MapCarter();

        return app;
    }
}