using Asp.Versioning;
using NexusGate.Infrastructure.Configurations;

namespace NexusGate.Configurations;

internal static class EndpointConfiguration
{
    public static IServiceCollection AddEndpointConfiguration(this IServiceCollection services)
    {
        services.AddEndpointVersioningConfiguration();
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

        globalGroup.UseMapEndpoints();
        return app;
    }
}