using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NexusGate.Infrastructure.Constants;
using NexusGate.Infrastructure.Limiters;

namespace NexusGate.Infrastructure.Configurations;

public static class RateLimiterConfiguration
{
    public static IServiceCollection AddRateLimiterConfiguration(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddPolicy(LimiterConstant.IpRateLimiter, IpRateLimiter.Partition);
        });
        
        return services;
    }

    public static IApplicationBuilder UseRateLimiterConfiguration(this IApplicationBuilder app)
    {
        app.UseRateLimiter();
        return app;
    }
}