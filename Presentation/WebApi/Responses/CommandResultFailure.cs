using System;
using WeatherForecastApp.WebApi.Properties;

namespace WeatherForecastApp.WebApi.Responses
{
    /// <summary>
    /// <inheritdoc cref="CommandResult"/>
    /// <para>
    /// The feedback for failed operation.
    /// </para>
    /// </summary>
    internal sealed record CommandResultFailure : CommandResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResultFailure"/> class.
        /// </summary>
        internal CommandResultFailure()
            : base(false, 0, Resource.RESPONSE_Command_Failure_NotChanged)
        {
        }

        /// <inheritdoc cref="CommandResultFailure()"/>
        /// <param name="exception">The exception captured during the workflow.</param>
        internal CommandResultFailure(Exception exception)
            : base(false, 0, $"{Resource.RESPONSE_Command_Failure_Error} | {exception.GetType} | {exception.Message}.")
        {
        }
    }
}
