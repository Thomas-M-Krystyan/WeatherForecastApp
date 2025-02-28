﻿using System;
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
        /// A content summarizing the result of the operation.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCommandResult"/> class.
        /// </summary>
        /// <param name="isSuccess"><inheritdoc cref="IsSuccess" path="/summary"/></param>
        /// <param name="changesCount"><inheritdoc cref="ChangesCount" path="/summary"/></param>
        /// <param name="content"><inheritdoc cref="Content" path="/summary"/></param>
        private QueryCommandResult(bool isSuccess, int changesCount, string content)
        {
            this.IsSuccess = isSuccess;
            this.ChangesCount = changesCount;
            this.Content = content;
        }

        /// <summary>
        /// The feedback for successful operation.
        /// </summary>
        /// <param name="changesCount"><inheritdoc cref="ChangesCount" path="/summary"/></param>
        public static QueryCommandResult Success(int changesCount)
            => new QueryCommandResult(true, changesCount, $"{Resources.RESPONSE_Command_Success} {changesCount}");

        /// <inheritdoc cref="Success(int)"/>
        /// <param name="results"><inheritdoc cref="Content" path="/summary"/></param>
        public static QueryCommandResult Success(params object[] results)
            => new QueryCommandResult(true, results.Length,
                $"{Resources.RESPONSE_Command_Success_Results} {results.Length} | {string.Join(", ", results)}");

        /// <summary>
        /// The feedback for failed operation.
        /// </summary>
        public static QueryCommandResult Failure()
            => new QueryCommandResult(false, 0, Resources.RESPONSE_Command_Failure_NotChanged);

        /// <inheritdoc cref="Failure()"/>
        /// <param name="message"><inheritdoc cref="Content" path="/summary"/></param>
        public static QueryCommandResult Failure(string message)
            => new QueryCommandResult(false, 0, message);

        /// <inheritdoc cref="Failure()"/>
        /// <param name="exception">The encountered exception.</param>
        public static QueryCommandResult Failure(Exception exception)
            => new QueryCommandResult(false, 0,
                $"{Resources.RESPONSE_Command_Failure_Error} | {exception.GetType().Name} | {exception.Message}.");

        /// <summary>
        /// The queried or looked up element is already existing.
        /// </summary>
        public static QueryCommandResult Existing()
            => new QueryCommandResult(false, 0, Resources.RESPONSE_Query_Failure_ExistingPK);

        /// <summary>
        /// Displays human-friendly result summary.
        /// </summary>
        public override string ToString() => this.Content;
    }
}
