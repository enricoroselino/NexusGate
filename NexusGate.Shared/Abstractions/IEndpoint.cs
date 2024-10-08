using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace NexusGate.Shared.Abstractions;

public interface IEndpoint
{
    public void AddRoutes(IEndpointRouteBuilder routeBuilder);
}