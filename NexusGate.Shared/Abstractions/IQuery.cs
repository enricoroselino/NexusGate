using MediatR;

namespace NexusGate.Shared.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
}