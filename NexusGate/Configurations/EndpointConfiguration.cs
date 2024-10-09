using Asp.Versioning;
using Carter;
using NexusGate.Infrastructure.Configurations;

namespace NexusGate.Configurations;

internal static class EndpointConfiguration
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

        var globalGroup = app.MapGroup("api/v{apiVersion:apiVersion}")
            .WithApiVersionSet(versionSet)
            .WithOpenApi();

        return globalGroup.MapCarter();
    }
}