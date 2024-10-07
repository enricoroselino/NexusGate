using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace NexusGate.Infrastructure.Configurations;

public static class SerilogConfiguration
{
    public static IServiceCollection AddSerilogConfiguration(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
        
        services.AddSerilog(Log.Logger);
        return services;
    }

    public static IApplicationBuilder UseSerilogConfiguration(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();
        return app;
    }
}