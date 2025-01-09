using FluentValidation;
using ProblemDetails = LookingPromos.SharedKernel.Models.ProblemDetails;

namespace LookingPromos.Middlewares.Exceptions;

/// <summary>
/// The middleware that handles exceptions and returns a formatted response to the client.
/// </summary>
/// <param name="next">The delegate of the request to continue the process if everything is ok</param>
/// <param name="logger">The logger to log the exceptions</param>
public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger
)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var details = GetExceptionDetails(exception);

            context.Response.StatusCode = details.Status;

            await context.Response.WriteAsJsonAsync(details);
        }
    }

    private static ProblemDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException
                => new ProblemDetails(
                    StatusCodes.Status400BadRequest,
                    "ValidationFailure",
                    "Validation error",
                    "One or more validation errors has occurred",
                    validationException.Errors
                ),
            _
                => new ProblemDetails(
                    StatusCodes.Status500InternalServerError,
                    "ServerError",
                    "Server error",
                    "An unexpected error has occurred",
                    null
                )
        };
    }
}
