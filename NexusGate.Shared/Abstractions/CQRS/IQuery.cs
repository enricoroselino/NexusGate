using MediatR;

namespace NexusGate.Shared.Abstractions.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
}