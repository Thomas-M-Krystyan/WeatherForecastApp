namespace WeatherForecastApp.WebApi.Responses
{
    /// <summary>
    /// A generic repository command response.
    /// </summary>
    public record QueryCommandResult(bool isSuccess, int changesCount, string message)
    {
        /// <summary>
        /// A status of the operation: success or failure.
        /// </summary>
        public bool IsSuccess { get; init; } = isSuccess;

        /// <summary>
        /// Number of rows / lines affected by the operation (database, CSV, text file).
        /// </summary>
        public int ChangesCount { get; init; } = changesCount;

        /// <summary>
        /// A message summarizing the resulf of the operation.
        /// </summary>
        public string Message { get; init; } = message;
    }
}
