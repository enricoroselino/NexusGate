using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NexusGate.Exceptions;

namespace NexusGate;

internal class GlobalExceptionHandler: IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            "Error Message: {@ExceptionMessage}, Occurred at:{@Time}"
            , exception.Message, DateTime.UtcNow);

        int statusCode = GetStatusCodeFromException(exception);
        httpContext.Response.StatusCode = statusCode;

        ProblemDetails problemDetails = new()
        {
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Status = statusCode,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationErrors", validationException.ValidationResult);
        }

        await httpContext
            .Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }

    private static int GetStatusCodeFromException(Exception exception) =>
        exception switch
        {
            InternalServerException => StatusCodes.Status500InternalServerError,
            ValidationException => StatusCodes.Status400BadRequest,
            BadRequestException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError,
        };

}