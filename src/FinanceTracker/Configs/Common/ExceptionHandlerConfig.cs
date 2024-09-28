using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Host.Configs.Common;

public static class ExceptionHandlerConfig
{
    public static void UseCustomExceptionHandler(
        this IApplicationBuilder app,
        IHostEnvironment environment
    )
    {
        if (environment.IsDevelopment())
        {
            app.Use(WriteDevelopmentResponse);
        }
        else
        {
            app.Use(WriteProductionResponse);
        }
    }

    private static Task WriteDevelopmentResponse(
        HttpContext httpContext,
        Func<Task> next
    ) => WriteResponse(httpContext, withDetails: true);

    private static Task WriteProductionResponse(
        HttpContext httpContext,
        Func<Task> next
    ) => WriteResponse(httpContext, withDetails: false);

    private static async Task WriteResponse(
        HttpContext httpContext,
        bool withDetails
    )
    {
        var exception = httpContext
            .Features.Get<IExceptionHandlerFeature>()
            ?.Error;
        if (exception is not null)
        {
            httpContext.Response.ContentType = "application/problem+json";

            await JsonSerializer.SerializeAsync(
                httpContext.Response.Body,
                new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = withDetails
                        ? $"An error occured: {exception.Message}"
                        : "An error occured",
                    Detail = withDetails ? exception.ToString() : null,
                    Extensions =
                    {
                        ["traceId"] =
                            Activity.Current?.Id
                            ?? httpContext.TraceIdentifier,
                    },
                }
            );
        }
    }
}
