using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Repository;
using WeatherForecastApp.Application.Responses;
using WeatherForecastApp.Domain.Models;
using WeatherForecastApp.Domain.Models.DTOs;
using WeatherForecastApp.WebApi.Responses;

namespace WeatherForecastApp.WebApi.Services.Forecasts
{
    internal sealed class PostForecastSingleCommand
    {
        private readonly IRepositoryContext<DbSet<WeatherForecastEntity>> _repositoryDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostForecastSingleCommand"/> class.
        /// </summary>
        /// <param name="repositoryDbContext">The repository database context.</param>
        public PostForecastSingleCommand(IRepositoryContext<DbSet<WeatherForecastEntity>> repositoryDbContext)
        {
            this._repositoryDbContext = repositoryDbContext;
        }

        internal async Task<CommandResult> ExecuteAsync(WeatherForecast dto, CancellationToken cancellationToken)
        {
            try
            {
                var entity = new WeatherForecastEntity
                {
                    DateTime = dto.DateTime,
                    TempCelsius = dto.TempCelsius.Value,
                    Description = dto.Description
                };

                using (this._repositoryDbContext)
                {
                    this._repositoryDbContext.Entities.Add(entity);

                    QueryResult result;
                    return (result = await this._repositoryDbContext.SaveChangesAsync(cancellationToken)).IsSuccess
                        ? new CommandResultSucces(result.ChangesCount)
                        : new CommandResultFailure();
                }
            }
            catch (Exception exception)
            {
                return new CommandResultFailure(exception);
            }
        }
    }
}
