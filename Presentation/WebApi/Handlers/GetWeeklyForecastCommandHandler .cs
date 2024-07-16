using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Handlers;
using WeatherForecastApp.Application.Responses;
using WeatherForecastApp.Domain.Converters;
using WeatherForecastApp.Domain.Resolvers.Interfaces;
using WeatherForecastApp.Persistence.Commands;

namespace WeatherForecastApp.WebApi.Handlers
{
    /// <summary>
    /// <inheritdoc cref="ICommandHandler{TData}"/>
    /// <para>
    /// Handles querying a weekly weather forecast from the repository.
    /// </para>
    /// </summary>
    internal sealed class GetWeeklyForecastCommandHandler : ICommandHandler<DateOnly>
    {
        private readonly IServiceResolver _serviceResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetWeeklyForecastCommandHandler"/> class.
        /// </summary>
        public GetWeeklyForecastCommandHandler(IServiceResolver serviceResolver)
        {
            this._serviceResolver = serviceResolver;
        }

        /// <inheritdoc cref="ICommandHandler{TData}.HandleAsync(TData, CancellationToken)"/>
        public async Task<QueryCommandResult> HandleAsync(DateOnly startDate, CancellationToken cancellationToken)
        {
            // Data
            DateTimeConverterLocalUtc utcConverter = this._serviceResolver.Resolve<DateTimeConverterLocalUtc>();
            DateTime utcDateTime = utcConverter.ConvertFrom(startDate.ToDateTime(default));
            DateOnly utcDate = DateOnly.FromDateTime(utcDateTime);

            // Command
            GetWeeklyForecastCommand command = this._serviceResolver.Resolve<GetWeeklyForecastCommand>();

            return await command.ExecuteAsync(utcDate, cancellationToken);
        }
    }
}
