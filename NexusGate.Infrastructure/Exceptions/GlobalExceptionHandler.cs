﻿using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NexusGate.Infrastructure.Exceptions;

internal class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
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

        // if (exception is ValidationException validationException)
        // {
        //     problemDetails.Extensions.Add("ValidationErrors", 
        //         validationException.Errors);
        // }

        await httpContext
            .Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }

    private static int GetStatusCodeFromException(Exception exception) =>
        exception switch
        {
            InternalServerException => StatusCodes.Status500InternalServerError,
            // ValidationException => StatusCodes.Status400BadRequest,
            // BadRequestException => StatusCodes.Status400BadRequest,
            // NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

}