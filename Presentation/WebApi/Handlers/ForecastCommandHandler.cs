using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Commands;
using WeatherForecastApp.Application.Handlers;
using WeatherForecastApp.Application.Responses;
using WeatherForecastApp.Domain.Converters;
using WeatherForecastApp.Domain.Models;
using WeatherForecastApp.Domain.Models.Units;
using WeatherForecastApp.Domain.Resolvers.Interfaces;
using WeatherForecastApp.WebApi.Models.DTOs;

namespace WeatherForecastApp.WebApi.Handlers
{
    /// <summary>
    /// <inheritdoc cref="ICommandHandler{TEntity, TModel, TDto}"/>
    /// <para>
    /// Handles commands associated with weather forecast.
    /// </para>
    /// </summary>
    internal sealed class ForecastCommandHandler : ICommandHandler<WeatherForecastEntity, WeatherForecast, WeatherForecastDto>
    {
        private readonly IServiceResolver _serviceResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ForecastCommandHandler"/> class.
        /// </summary>
        public ForecastCommandHandler(IServiceResolver serviceResolver)
        {
            this._serviceResolver = serviceResolver;
        }

        /// <inheritdoc cref="ICommandHandler{TEntity, TModel, TDto}.HandleAsync{TCommand}(TDto, CancellationToken)"/>
        public async Task<QueryCommandResult> HandleAsync<TCommand>(WeatherForecastDto dto, CancellationToken cancellationToken)
            where TCommand : class, IQueryCommand<WeatherForecastEntity, WeatherForecast>
        {
            // Converters
            TemperatureConverterCF tempConverter = this._serviceResolver.Resolve<TemperatureConverterCF>();
            TemperatureConverterCFeel feelConverter = this._serviceResolver.Resolve<TemperatureConverterCFeel>();

            // Data
            WeatherForecast forecast;

            if (dto.Scale == TemperatureScales.Celsius)
            {
                TemperatureCelsius tempCelsiusUnit = new(dto.Temperature);

                forecast = new(
                    date: dto.DateTime,
                    tempC: dto.Temperature,
                    tempF: tempConverter.ConvertFrom(tempCelsiusUnit).Value,
                    description: feelConverter.ConvertFrom(tempCelsiusUnit).ToString());
            }
            else
            {
                TemperatureFahrenheit tempFahrenheitUnit = new(dto.Temperature);
                TemperatureCelsius tempCelsiusUnit = tempConverter.ConvertBack(tempFahrenheitUnit);

                forecast = new(
                    date: dto.DateTime,
                    tempC: tempCelsiusUnit.Value,
                    tempF: dto.Temperature,
                    description: feelConverter.ConvertFrom(tempCelsiusUnit).ToString());
            }

            // Command
            TCommand queryCommand = this._serviceResolver.Resolve<TCommand>();

            return await queryCommand.ExecuteAsync(forecast, cancellationToken);
        }
    }
}
