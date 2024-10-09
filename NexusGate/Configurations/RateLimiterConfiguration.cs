using NexusGate.Configurations.Limiters;
using NexusGate.Constants;

namespace NexusGate.Configurations;

public static class RateLimiterConfiguration
{
    public static IServiceCollection AddRateLimiterConfiguration(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddPolicy(LimiterPolicyConstant.IpRateLimiter, IpRateLimiter.Partition);
        });
        
        return services;
    }

    public static IApplicationBuilder UseRateLimiterConfiguration(this IApplicationBuilder app)
    {
        app.UseRateLimiter();
        return app;
    }
}