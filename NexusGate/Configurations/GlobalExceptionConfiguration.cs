namespace NexusGate.Configurations;

internal static class GlobalExceptionConfiguration
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