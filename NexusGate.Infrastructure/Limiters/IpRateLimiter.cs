using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Http;

namespace NexusGate.Infrastructure.Limiters;

public static class IpRateLimiter
{
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