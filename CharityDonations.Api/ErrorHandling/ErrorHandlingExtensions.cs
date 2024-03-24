using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CharityDonations.Api.ErrorHandling;

public static class ErrorHandlingExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            var logger = context.RequestServices.GetRequiredService<ILoggerFactory>()
                                .CreateLogger("Error Handling");

            var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionDetails?.Error;

            logger.LogError(exception, "Could not process a request on machine {Machine}. TraceID: {TraceId}",
            Environment.MachineName, Activity.Current?.TraceId);

            var problem = new ProblemDetails
            {
                Title = "An error occurred while processing the request.",
                Status = StatusCodes.Status500InternalServerError,
                Extensions =
                {
                    {"traceId", Activity.Current?.TraceId.ToString()}
                }
            };

            var environment = context.RequestServices.GetRequiredService<IHostEnvironment>();

            if (environment.IsDevelopment())
            {
                problem.Detail = exception?.ToString();
            }

            await Results.Problem(problem).ExecuteAsync(context);
        });
    }
}
