using FinanceTracker.Host;
using FinanceTracker.Host.Helpers;
using FinanceTracker.Host.Providers;

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
    startupLogger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    startupLogger.Dispose();
}
