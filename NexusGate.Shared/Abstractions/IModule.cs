using Microsoft.Extensions.DependencyInjection;

namespace NexusGate.Shared.Abstractions;

public interface IModule
{
    IServiceCollection RegisterModule(IServiceCollection builder);
}