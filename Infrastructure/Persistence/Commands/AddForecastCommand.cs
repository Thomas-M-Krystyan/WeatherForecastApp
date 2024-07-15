using Microsoft.Extensions.Logging;
using System;
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
    /// Adds new weather forecast to the repository.
    /// </para>
    /// </summary>
    public sealed class AddForecastCommand : IQueryCommand<WeatherForecastEntity, WeatherForecast>
    {
        private readonly IServiceResolver _serviceResolver;
        private readonly ILogger<AddForecastCommand> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddForecastCommand"/> class.
        /// </summary>
        public AddForecastCommand(IServiceResolver serviceResolver, ILogger<AddForecastCommand> logger)
        {
            this._serviceResolver = serviceResolver;
            this._logger = logger;
        }

        /// <inheritdoc cref="IQueryCommand{TEntity, TModel}.ExecuteAsync(TModel, CancellationToken)"/>
        public async Task<QueryCommandResult> ExecuteAsync(WeatherForecast model, CancellationToken cancellationToken)
        {
            return await Caller.SafeExecute(async () =>
            {
                // Preparing a repository entity
                WeatherForecastEntity entity = new()
                {
                    Date = DateOnly.FromDateTime(model.DateTime),
                    TempCelsius = model.TempCelsius.Value,
                    Description = model.Description
                };

                // Modifying a repository
                WeatherForecastContext repositoryContext = this._serviceResolver.Resolve<WeatherForecastContext>();

                repositoryContext.Entities.Add(entity);

                QueryResult result;
                return (result = await repositoryContext.SaveChangesAsync(cancellationToken)).IsSuccess
                    ? QueryCommandResult.Success(result.ChangesCount)
                    : QueryCommandResult.Failure();
            },
            QueryCommandResult.Failure, this._logger);
        }
    }
}
