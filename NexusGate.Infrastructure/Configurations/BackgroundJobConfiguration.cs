using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace NexusGate.Infrastructure.Configurations;

public static class BackgroundJobConfiguration
{
    public static IServiceCollection AddBackgroundJobConfiguration(this IServiceCollection services)
    {
        services.AddQuartz();
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = false;
        });

        return services;
    }
}