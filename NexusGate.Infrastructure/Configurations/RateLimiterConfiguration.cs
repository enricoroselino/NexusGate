using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NexusGate.Infrastructure.Limiters;
using NexusGate.Shared.Constants;

namespace NexusGate.Infrastructure.Configurations;

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