using MediatR;

namespace NexusGate.Behaviors;

internal class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var responseName = typeof(TResponse).Name;

        const string startTemplate =
            "[START] Handle Request{@RequestName} - Response={@ResponseName} - RequestData={@RequestData}";
        const string endTemplate = "[END] Handled {@Request} with {@Response}";
        _logger.LogInformation(startTemplate, requestName, responseName, request);
        var response = await next();
        _logger.LogInformation(endTemplate, requestName, responseName);

        return response;
    }
}