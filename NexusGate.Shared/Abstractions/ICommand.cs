using MediatR;

namespace NexusGate.Shared.Abstractions;

public interface ICommand : ICommand<Unit>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}