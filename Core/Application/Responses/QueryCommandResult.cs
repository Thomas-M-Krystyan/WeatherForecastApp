using System;
using WeatherForecastApp.Application.Properties;

namespace WeatherForecastApp.Application.Responses
{
    /// <summary>
    /// A generic repository command response.
    /// </summary>
    public readonly struct QueryCommandResult
    {
        /// <summary>
        /// A status of the operation: success or failure.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Number of rows / lines affected by the operation (database, CSV, text file).
        /// </summary>
        public int ChangesCount { get; }

        /// <summary>
        /// A message summarizing the result of the operation.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCommandResult"/> class.
        /// </summary>
        /// <param name="isSuccess"><inheritdoc cref="IsSuccess" path="/summary"/></param>
        /// <param name="changesCount"><inheritdoc cref="ChangesCount" path="/summary"/></param>
        /// <param name="message"><inheritdoc cref="Message" path="/summary"/></param>
        private QueryCommandResult(bool isSuccess, int changesCount, string message)
        {
            this.IsSuccess = isSuccess;
            this.ChangesCount = changesCount;
            this.Message = message;
        }

        /// <summary>
        /// The feedback for successful operation.
        /// </summary>
        public static QueryCommandResult Success(int changesCount)
            => new QueryCommandResult(true, changesCount, $"{Resource.RESPONSE_Command_Success} {changesCount}");

        /// <summary>
        /// The feedback for failed operation.
        /// </summary>
        public static QueryCommandResult Failure()
            => new QueryCommandResult(false, 0, Resource.RESPONSE_Command_Failure_NotChanged);

        /// <inheritdoc cref="Failure()"/>
        /// <param name="message">The result message (e.g., from validation).</param>
        public static QueryCommandResult Failure(string message)
            => new QueryCommandResult(false, 0, message);

        /// <inheritdoc cref="Failure()"/>
        /// <param name="exception">The encountered exception.</param>
        public static QueryCommandResult Failure(Exception exception)
            => new QueryCommandResult(false, 0,
                $"{Resource.RESPONSE_Command_Failure_Error} | {exception.GetType().Name} | {exception.Message}.");

        /// <summary>
        /// Displays human-friendly result summary.
        /// </summary>
        public override string ToString() => this.Message;
    }
}
