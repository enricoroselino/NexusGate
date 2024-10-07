using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace NexusGate.Infrastructure.Configurations;

public static class FluentValidationConfiguration
{
    public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}