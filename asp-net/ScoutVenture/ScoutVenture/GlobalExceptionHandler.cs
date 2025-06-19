using Microsoft.AspNetCore.Diagnostics;
using NamiClient.Exceptions;

namespace ScoutVenture
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogDebug(exception, exception.Message);
            Microsoft.AspNetCore.Mvc.ProblemDetails problemDetails;
            switch (exception)
            {
                case NamiAuthenticationException:
                    problemDetails = ProblemDetails.New(exception.Message, StatusCodes.Status401Unauthorized);
                    break;
                case NamiAccessViolationException:
                    problemDetails = ProblemDetails.New(exception.Message, StatusCodes.Status403Forbidden);
                    break;
                default:
                    logger.LogError(exception, exception.Message);
                    problemDetails = ProblemDetails.New("Internal Server Error",
                        StatusCodes.Status500InternalServerError,
                        "If this error persists, please contact your administrator.");
                    break;
            }

            httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails!, cancellationToken);
            return true;
        }
    }
}