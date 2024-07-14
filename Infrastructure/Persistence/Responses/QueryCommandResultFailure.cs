using System;
using WeatherForecastApp.Persistence.Properties;

namespace WeatherForecastApp.Persistence.Responses
{
    /// <summary>
    /// <inheritdoc cref="QueryCommandResult"/>
    /// <para>
    /// The feedback for failed operation.
    /// </para>
    /// </summary>
    public sealed record QueryCommandResultFailure : QueryCommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCommandResultFailure"/> class.
        /// </summary>
        public QueryCommandResultFailure()
            : base(false, 0, Resource.RESPONSE_Command_Failure_NotChanged)
        {
        }

        /// <inheritdoc cref="QueryCommandResultFailure()"/>
        /// <param name="exception">The exception captured during the workflow.</param>
        public QueryCommandResultFailure(Exception exception)
            : base(false, 0, $"{Resource.RESPONSE_Command_Failure_Error} | {exception.GetType} | {exception.Message}.")
        {
        }
    }
}
