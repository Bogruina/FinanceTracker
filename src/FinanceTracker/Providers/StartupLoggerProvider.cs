using FinanceTracker.Host.Interfaces;
using NLog;

namespace FinanceTracker.Host.Providers;

public class StartupLoggerProvider
{
    /// <summary>
    /// Returns instance of the <see cref="IStartupLogger"/> class.
    /// </summary>
    public static IStartupLogger GetLogger()
    {
        if (LoggerProvider.IsConfigured())
        {
            return new StartupLogger();
        }

        throw new Exception("The logger provider is not configured");
    }

    /// <inheritdoc cref="IStartupLogger"/>
    private class StartupLogger : IStartupLogger
    {
        private readonly Logger _logger =
            LogManager.GetCurrentClassLogger();

        #region IStartupLogger

        /// <inheritdoc cref="IStartupLogger.Debug"/>
        public void Debug(string msg) => _logger.Debug(msg);

        /// <inheritdoc cref="IStartupLogger.Warn"/>
        public void Warn(string msg) => _logger.Warn(msg);

        /// <inheritdoc cref="IStartupLogger.Error"/>
        public void Error(Exception e, string msg) =>
            _logger.Error(e, msg);

        #endregion IStartupLogger

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            LogManager.Shutdown();
        }

        #endregion
    }
}
