using Mediator;

namespace NexusGate.Configurations.Behaviors;

internal class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
    {
        var requestName = typeof(TRequest).Name;
        var responseName = typeof(TResponse).Name;
        
        const string startTemplate =
            "[START] Handle Request{@RequestName} - Response={@ResponseName} - RequestData={@message}";
        const string endTemplate = "[END] Handled {@Request} with {@Response}";
        _logger.LogInformation(startTemplate, requestName, responseName, message);
        var response = await next(message, cancellationToken);
        _logger.LogInformation(endTemplate, requestName, responseName);

        return response;
    }
}