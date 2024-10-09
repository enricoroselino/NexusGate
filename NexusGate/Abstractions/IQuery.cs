﻿using Mediator;

namespace NexusGate.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
}