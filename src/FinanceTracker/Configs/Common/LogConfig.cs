using NLog.Web;

namespace FinanceTracker.Host.Configs;

public static class LogConfig
{
    public static void AddNLogConfig(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Host.UseNLog();
    }
}
