using Quartz;

namespace NexusGate.Configurations;

internal static class BackgroundJobConfiguration
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