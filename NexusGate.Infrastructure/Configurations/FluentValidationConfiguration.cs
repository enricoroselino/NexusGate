using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace NexusGate.Infrastructure.Configurations;

public static class FluentValidationConfiguration
{
    public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}