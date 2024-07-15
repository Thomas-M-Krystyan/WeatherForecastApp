using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Commands;
using WeatherForecastApp.Application.Handlers;
using WeatherForecastApp.Application.Responses;
using WeatherForecastApp.Domain.Models;
using WeatherForecastApp.Domain.Resolvers.Interfaces;
using WeatherForecastApp.Domain.Respones;
using WeatherForecastApp.Domain.Validators;

namespace WeatherForecastApp.WebApi.Handlers
{
    /// <summary>
    /// <inheritdoc cref="ICommandHandler{TEntity, TModel, TDto}"/>
    /// <para>
    /// Handles querying a weekly weather forecast from the repository.
    /// </para>
    /// </summary>
    internal sealed class GetWeeklyForecastCommandHandler : ICommandHandler<WeatherForecastEntity, DateTime, DateOnly>
    {
        private readonly IServiceResolver _serviceResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetWeeklyForecastCommandHandler"/> class.
        /// </summary>
        public GetWeeklyForecastCommandHandler(IServiceResolver serviceResolver)
        {
            this._serviceResolver = serviceResolver;
        }

        /// <inheritdoc cref="ICommandHandler{TEntity, TModel, TDto}.HandleAsync{TCommand}(TDto, CancellationToken)"/>
        public async Task<QueryCommandResult> HandleAsync<TCommand>(DateOnly startDate, CancellationToken cancellationToken)
            where TCommand : class, IQueryCommand<WeatherForecastEntity, DateTime>
        {
            // Data
            var dateTime = startDate.ToDateTime(default);

            // Validation
            DateValidator validator = this._serviceResolver.Resolve<DateValidator>();
            ValidatorResponse validationResult = validator.Validate(dateTime);

            if (validationResult.IsInvalid)
            {
                return QueryCommandResult.Failure(validationResult.Message);
            }

            // Command
            TCommand command = this._serviceResolver.Resolve<TCommand>();

            return await command.ExecuteAsync(dateTime, cancellationToken);
        }
    }
}
