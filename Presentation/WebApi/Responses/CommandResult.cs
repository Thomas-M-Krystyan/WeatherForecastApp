namespace WeatherForecastApp.WebApi.Responses
{
    /// <summary>
    /// A generic repository command response.
    /// </summary>
    internal record CommandResult(bool isSuccess, int changesCount, string message)
    {
        /// <summary>
        /// A status of the operation: success or failure.
        /// </summary>
        internal bool IsSuccess { get; init; } = isSuccess;

        /// <summary>
        /// Number of rows / lines affected by the operation (database, CSV, text file).
        /// </summary>
        internal int ChangesCount { get; init; } = changesCount;

        /// <summary>
        /// A message summarizing the resulf of the operation.
        /// </summary>
        internal string Message { get; init; } = message;
    }
}
