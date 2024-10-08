using NexusGate.Shared.Abstractions;

namespace NexusGate;

public class TestEndpoint
{
    protected TestEndpoint()
    {
    }

    protected static async Task<IResult> Greet()
    {
        return await Task.FromResult(Results.Ok("Hello"));
    }
}

public class TestRouteBuilder : TestEndpoint, IEndpoint
{
    public virtual void AddRoutes(IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup("greet");
        group.MapGet("/hello", Greet);
    }
}