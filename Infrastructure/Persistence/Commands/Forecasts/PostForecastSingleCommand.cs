using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Repository;
using WeatherForecastApp.Application.Responses;
using WeatherForecastApp.Domain.Models;
using WeatherForecastApp.Domain.Models.DTOs;
using WeatherForecastApp.Persistence.Commands.Base;
using WeatherForecastApp.Persistence.Responses;

namespace WeatherForecastApp.Persistence.Commands.Forecasts
{
    internal sealed class PostForecastSingleCommand : QueryCommand<WeatherForecastEntity, WeatherForecast>
    {
        /// <inheritdoc cref="QueryCommand{TEntity, TModel}.ExecuteAsync(TModel, CancellationToken)"/>
        public override async Task<QueryCommandResult> ExecuteAsync(
            IRepositoryContext<DbSet<WeatherForecastEntity>> repositoryContext, WeatherForecast dto, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new WeatherForecastEntity
                {
                    DateTime = dto.DateTime,
                    TempCelsius = dto.TempCelsius.Value,
                    Description = dto.Description
                };

                repositoryContext.Entities.Add(entity);

                QueryResult result;
                return (result = await repositoryContext.SaveChangesAsync(cancellationToken)).IsSuccess
                    ? new QueryCommandResultSucces(result.ChangesCount)
                    : new QueryCommandResultFailure();
            }
            catch (Exception exception)
            {
                return new QueryCommandResultFailure(exception);
            }
        }
    }
}
