using NexusGate.Shared.Abstractions;

namespace NexusGate.Endpoints;

public partial class TestEndpoint : IEndpoint
{
    public virtual void AddRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup("greet");
        group.MapGet("/hello", Greet);
    }
}

public partial class TestEndpoint
{
    private static async Task<IResult> Greet()
    {
        return await Task.FromResult(Results.Ok("Hello"));
    }
}