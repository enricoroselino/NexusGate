namespace NexusGate.Modules;

public interface IModule
{
    IServiceCollection RegisterModule(IServiceCollection builder);
}