using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Responses;
using WeatherForecastApp.Domain.Models;
using WeatherForecastApp.Domain.Models.DTOs;
using WeatherForecastApp.Domain.Resolvers.Interfaces;
using WeatherForecastApp.Persistence.Commands.Base;
using WeatherForecastApp.WebApi.Responses;

namespace WeatherForecastApp.Persistence.Commands.Forecasts
{
    internal sealed class PostForecastSingleCommand : QueryCommand<WeatherForecastEntity, WeatherForecast>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostForecastSingleCommand"/> class.
        /// </summary>
        public PostForecastSingleCommand(IServiceResolver serviceResolver)
            : base(serviceResolver)
        {
        }

        /// <inheritdoc cref="QueryCommand{TEntity, TModel}.ExecuteAsync(TModel, CancellationToken)"/>
        public override async Task<QueryCommandResult> ExecuteAsync(WeatherForecast dto, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new WeatherForecastEntity
                {
                    DateTime = dto.DateTime,
                    TempCelsius = dto.TempCelsius.Value,
                    Description = dto.Description
                };

                this.RepositoryContext.Entities.Add(entity);

                RepositoryResult result;
                return (result = await this.RepositoryContext.SaveChangesAsync(cancellationToken)).IsSuccess
                    ? new QueryCommandResultSucces(result.ChangesCount)
                    : new QueryCommandResultFailure();
            }
            catch (Exception exception)
            {
                return new QueryCommandResultFailure(exception);
            }
            finally
            {
                this.RepositoryContext.Dispose();
            }
        }
    }
}
