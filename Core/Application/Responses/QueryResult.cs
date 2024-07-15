namespace WeatherForecastApp.Application.Responses
{
    /// <summary>
    /// A generic repository command response.
    /// </summary>
    public readonly struct QueryResult
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
        /// Initializes a new instance of the <see cref="QueryResult"/> struct.
        /// </summary>
        /// <param name="isSuccess"><inheritdoc cref="IsSuccess" path="/summary"/></param>
        /// <param name="changesCount"><inheritdoc cref="ChangesCount" path="/summary"/></param>
        private QueryResult(bool isSuccess, int changesCount)
        {
            this.IsSuccess = isSuccess;
            this.ChangesCount = changesCount;
        }

        /// <summary>
        /// The feedback for successful query.
        /// </summary>
        public static QueryResult Success(int changesCount)
            => new QueryResult(true, changesCount);

        /// <summary>
        /// The feedback for failed query.
        /// </summary>
        public static QueryResult Failure()
            => new QueryResult(false, 0);
    }
}
