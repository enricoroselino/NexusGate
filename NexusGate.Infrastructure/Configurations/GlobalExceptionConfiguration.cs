using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NexusGate.Infrastructure.Configurations;

public static class GlobalExceptionConfiguration
{
    public static IServiceCollection AddGlobalExceptionConfiguration(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        return services;
    }

    public static IApplicationBuilder UseGlobalExceptionConfiguration(this IApplicationBuilder app)
    {
        app.UseExceptionHandler();
        return app;
    }
}