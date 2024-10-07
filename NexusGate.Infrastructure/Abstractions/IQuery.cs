using MediatR;

namespace NexusGate.Infrastructure.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
}