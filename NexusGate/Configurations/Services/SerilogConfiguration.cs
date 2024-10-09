using Serilog;
using Serilog.Events;

namespace NexusGate.Configurations.Services;

public static class SerilogConfiguration
{
    public static IServiceCollection AddLoggerConfiguration(this IServiceCollection services)
    {
        var seqEndpoint = Environment.GetEnvironmentVariable("SEQ_ENDPOINT") ??
                          throw new InvalidOperationException("SEQ_ENDPOINT NOT SET.");

        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq(seqEndpoint);

        Log.Logger = loggerConfiguration.CreateLogger();
        services.AddSerilog(Log.Logger);
        return services;
    }

    public static IApplicationBuilder UseLoggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();
        return app;
    }
}