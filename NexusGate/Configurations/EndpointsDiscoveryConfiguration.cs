using NexusGate.Infrastructure;
using NexusGate.Shared;
using NexusGate.Shared.Abstractions;

namespace NexusGate.Configurations;

public static class EndpointsDiscoveryConfiguration
{
    public static IEndpointRouteBuilder UseMapEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var endpoints = DiscoverClasses.Search<IEndpoint>();
        
        foreach (var endpoint in endpoints)
        {
            endpoint.AddRoutes(routeBuilder);
        }
        
        return routeBuilder;
    }
}