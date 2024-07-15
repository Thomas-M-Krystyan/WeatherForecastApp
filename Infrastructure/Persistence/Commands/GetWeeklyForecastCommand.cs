using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Commands;
using WeatherForecastApp.Application.Responses;
using WeatherForecastApp.Domain.Models;
using WeatherForecastApp.Domain.Resolvers.Interfaces;
using WeatherForecastApp.Domain.Utilities;
using WeatherForecastApp.Persistence.Context;

namespace WeatherForecastApp.Persistence.Commands
{
    /// <summary>
    /// <inheritdoc cref="IQueryCommand{TEntity, TModel}"/>
    /// <para>
    /// Gets up to 7 weather forecasts from the repository starting from the given date.
    /// </para>
    /// </summary>
    public sealed class GetWeeklyForecastCommand : IQueryCommand<WeatherForecastEntity, DateOnly>
    {
        private readonly IServiceResolver _serviceResolver;
        private readonly ILogger<GetWeeklyForecastCommand> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetWeeklyForecastCommand"/> class.
        /// </summary>
        public GetWeeklyForecastCommand(IServiceResolver serviceResolver, ILogger<GetWeeklyForecastCommand> logger)
        {
            this._serviceResolver = serviceResolver;
            this._logger = logger;
        }

        /// <inheritdoc cref="IQueryCommand{TEntity, TModel}.ExecuteAsync(TModel, CancellationToken)"/>
        public Task<QueryCommandResult> ExecuteAsync(DateOnly startDate, CancellationToken _)
        {
            return Caller.SafeExecute(() =>
            {
                // Querying repository
                WeatherForecastContext repositoryContext = this._serviceResolver.Resolve<WeatherForecastContext>();
                
                WeatherForecastEntity[] queriedForecasts = [.. repositoryContext.Entities
                    .AsNoTracking()
                    .Where(forecast => forecast.Date >= startDate)
                    .Take(7)];

                return Task.FromResult(queriedForecasts.Length > 0
                    ? QueryCommandResult.Success(queriedForecasts)
                    : QueryCommandResult.Failure());
            },
            QueryCommandResult.Failure, this._logger);
        }
    }
}
