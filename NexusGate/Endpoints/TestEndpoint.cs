using Carter;
using NexusGate.Shared.Abstractions;

namespace NexusGate.Endpoints;

public partial class TestEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("greet");
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