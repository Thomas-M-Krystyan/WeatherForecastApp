using Microsoft.Extensions.Logging;
using System;

namespace WeatherForecastApp.Domain.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ILogger{TCategoryName}"/> interface.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Logs the details about the exception as <see cref="LogLevel.Critical"/>.
        /// </summary>
        /// <typeparam name="TCategoryName">The type of the instance bound to the logger.</typeparam>
        /// <param name="logger">The logger to be used.</param>
        /// <param name="exception">The exception to be logged.</param>
        public static void LogDetailed<TCategoryName>(this ILogger<TCategoryName> logger, Exception exception)
            => logger.LogCritical("{log}", exception.ToString());
    }
}
