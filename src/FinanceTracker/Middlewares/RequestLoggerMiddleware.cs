using System.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;

namespace FinanceTracker.Host.Middlewares;

public class RequestLoggerMiddleware
{
    private readonly ILogger<RequestLoggerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public RequestLoggerMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggerMiddleware> logger
    )
    {
        _logger = logger;
        _next = next;
    }

    #region Middleware component

    /// <summary>
    /// Logs request, then call the next middleware in the HTTP request processing pipeline, and then logs response.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/>.</param>
    public async Task Invoke(HttpContext context)
    {
        LogRequest(context, context.Request);

        await _next(context);

        LogResponse(context, context.Request, context.Response);
    }

    #endregion

    #region Private methods

    private void LogRequest(HttpContext context, HttpRequest request)
    {
        _logger.LogDebug(
            "{HttpMethod} {Url} {Ip} traceId {TraceId}",
            request.Method,
            GetUrl(request),
            GetRemoteIp(request),
            GetTraceId(context)
        );
    }

    private void LogResponse(
        HttpContext context,
        HttpRequest request,
        HttpResponse response
    )
    {
        _logger.LogDebug(
            "{HttpMethod} {Url} {StatusCode} {Ip} traceId {TraceId}",
            request.Method,
            GetUrl(request),
            response.StatusCode,
            GetRemoteIp(request),
            GetTraceId(context)
        );
    }

    private static string GetRemoteIp(HttpRequest request)
    {
        var remoteIp =
            request.HttpContext.Connection.RemoteIpAddress?.ToString();

        if (request.Headers.TryGetValue("X-Forwarded-For", out var values))
        {
            remoteIp = values.FirstOrDefault();
        }

        return remoteIp ?? "";
    }

    private static string GetUrl(HttpRequest request)
    {
        var url = request.GetDisplayUrl();
        var index = url.IndexOf("/api/", StringComparison.Ordinal);
        return index > 0 ? url[(index + "/api/".Length)..] : url;
    }

    private static string GetTraceId(HttpContext context)
    {
        return Activity.Current?.Id ?? context.TraceIdentifier;
    }

    #endregion
}
