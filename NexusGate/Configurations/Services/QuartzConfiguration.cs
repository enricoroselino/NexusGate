using Quartz;

namespace NexusGate.Configurations.Services;

internal static class QuartzConfiguration
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