using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Ride.API.Exceptions;

internal sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception occurred");

        var problemDetails = exception switch
        {
            ValidationException validationException => new ProblemDetails
            {
                Type = "ValidationFailure",
                Title = "One or more validation errors occurred",
                Status = StatusCodes.Status400BadRequest,
                Extensions = new Dictionary<string, object?>
                {
                    ["errors"] = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        )
                }
            },
            ApplicationException => new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message
            },
            _ => new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "An error occurred",
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception.Message
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problemDetails
        });
    }
}
