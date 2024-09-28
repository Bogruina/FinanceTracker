using FinanceTracker.IdentityServer;
using FinanceTracker.IdentityServer.Helpers;
using FinanceTracker.IdentityServer.Providers;

LoggerProvider.Configure(
    ConfigurationHelper.BuildAppConfiguration(
        ConfigurationHelper.GetAspNetCoreEnvironmentName()
    )
);

var startupLogger = StartupLoggerProvider.GetLogger();

try
{
    startupLogger.Debug("Program: application configure started");

    var builder = WebApplication.CreateBuilder(args);
    builder.UseLoggerProvider();
    builder.ConfigureServices(startupLogger);

    startupLogger.Debug("Program: application build started");

    var app = builder.Build();
    app.Configure();

    startupLogger.Debug(
        "Program: application has been successfully launched"
    );

    app.Run();
}
catch (Exception ex)
{
    startupLogger.Error(ex, "Program: failed to launch the application");
    throw;
}
finally
{
    startupLogger.Dispose();
}
