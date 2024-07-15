using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WeatherForecastApp.Domain.Extensions;

namespace WeatherForecastApp.Domain.Utilities
{
    /// <summary>
    /// Helper class used to call business logic in a standardized way.
    /// </summary>
    public static class Caller
    {
        /// <summary>
        /// Executes the provided logic returning <typeparamref name="TResult"/> surrounded by try-catch block + logging.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TCategoryName">The type of the logger.</typeparam>
        /// <param name="logic">The logic to be executed.</param>
        /// <param name="fallbackResult">The fallback result.</param>
        /// <param name="logger">The logger to be used.</param>
        /// <returns>
        /// <list type="bullet">
        ///   <item>In case of success: The initial result of the operation.</item>
        ///   <item>In case of failure: The fallback result of the operation.</item>
        /// </list>
        /// </returns>
        public static async Task<TResult> SafeExecute<TResult, TCategoryName>(
            Func<Task<TResult>> logic, Func<Exception, TResult> fallbackResult, ILogger<TCategoryName> logger)
        {
            try
            {
                return await logic.Invoke();
            }
            catch (AggregateException exceptions)
            {
                foreach (Exception exception in exceptions.InnerExceptions)
                {
                    logger.LogDetailed(exception);
                }

                return fallbackResult.Invoke(exceptions.InnerException!);
            }
        }
    }
}
