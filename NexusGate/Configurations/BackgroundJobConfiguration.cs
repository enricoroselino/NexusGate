using Quartz;

namespace NexusGate.Configurations;

public static class BackgroundJobConfiguration
{
    public static IServiceCollection AddBackgroundJobConfiguration(this IServiceCollection services)
    {
        services.AddQuartz();
        services.AddQuartzHostedService(options =>
        {
            // wait if any background job running, when closing.
            options.WaitForJobsToComplete = true;
        });

        return services;
    }
}