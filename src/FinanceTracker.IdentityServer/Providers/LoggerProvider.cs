using FinanceTracker.IdentityServer.Configs;

namespace FinanceTracker.IdentityServer.Providers;

public static class LoggerProvider
{
    private static bool _isConfigured;

    public static bool IsConfigured() => _isConfigured;

    public static void Configure(IConfiguration configuration)
    {
        NLogConfig.AddNLogConfig(configuration);

        _isConfigured = true;
    }

    public static void UseLoggerProvider(
        this WebApplicationBuilder builder
    )
    {
        builder.UseNLogAsLoggerProvider();
    }
}
