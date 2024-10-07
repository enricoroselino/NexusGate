using System.Threading.RateLimiting;

namespace NexusGate.Configurations;

public static class RateLimiterConfiguration
{
    public static IServiceCollection AddRateLimiterConfiguration(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddPolicy(IpRateLimiter.Name, IpRateLimiter.Partition);
        });
        
        return services;
    }

    public static IApplicationBuilder UseRateLimiterConfiguration(this IApplicationBuilder app)
    {
        app.UseRateLimiter();
        return app;
    }
}

public static class IpRateLimiter
{
    public const string Name = nameof(IpRateLimiter);

    public static RateLimitPartition<string?> Partition(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        var option = new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10,
            Window = TimeSpan.FromSeconds(10)
        };
        
        return RateLimitPartition.GetFixedWindowLimiter(partitionKey: ipAddress, factory: _ => option);
    }
}