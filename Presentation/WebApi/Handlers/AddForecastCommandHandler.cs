using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Handlers;
using WeatherForecastApp.Application.Responses;
using WeatherForecastApp.Domain.Converters;
using WeatherForecastApp.Domain.Models;
using WeatherForecastApp.Domain.Models.Units;
using WeatherForecastApp.Domain.Resolvers.Interfaces;
using WeatherForecastApp.Domain.Respones;
using WeatherForecastApp.Domain.Validators;
using WeatherForecastApp.Persistence.Commands;
using WeatherForecastApp.WebApi.Enums;
using WeatherForecastApp.WebApi.Models.DTOs;

namespace WeatherForecastApp.WebApi.Handlers
{
    /// <summary>
    /// <inheritdoc cref="ICommandHandler{TData}"/>
    /// <para>
    /// Handles adding single weather forecast to the repository.
    /// </para>
    /// </summary>
    internal sealed class AddForecastCommandHandler : ICommandHandler<WeatherForecastDto>
    {
        private readonly IServiceResolver _serviceResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddForecastCommandHandler"/> class.
        /// </summary>
        public AddForecastCommandHandler(IServiceResolver serviceResolver)
        {
            this._serviceResolver = serviceResolver;
        }

        /// <inheritdoc cref="ICommandHandler{TData}.HandleAsync(TData, CancellationToken)"/>
        public async Task<QueryCommandResult> HandleAsync(WeatherForecastDto dto, CancellationToken cancellationToken)
        {
            // Validation #1
            DateValidator dateValidator = this._serviceResolver.Resolve<DateValidator>();
            ValidatorResponse dateValidationResult = dateValidator.Validate(dto.Date.ToDateTime(default));

            if (dateValidationResult.IsInvalid)
            {
                return QueryCommandResult.Failure(dateValidationResult.Message);
            }

            // Converters
            DateTimeConverterLocalUtc utcConverter = this._serviceResolver.Resolve<DateTimeConverterLocalUtc>();
            TemperatureConverterCF tempConverter = this._serviceResolver.Resolve<TemperatureConverterCF>();
            TemperatureConverterCFeel feelConverter = this._serviceResolver.Resolve<TemperatureConverterCFeel>();

            // Data
            WeatherForecast forecast;

            if (dto.Scale == TemperatureScales.Celsius)
            {
                TemperatureCelsius tempCelsiusUnit = new(dto.Temperature);

                forecast = new(
                    date: utcConverter.ConvertFrom(dto.Date.ToDateTime(default)),  // NOTE: Ensures to always store time in the database in UTC format
                    tempC: dto.Temperature,
                    tempF: tempConverter.ConvertFrom(tempCelsiusUnit).Value,
                    description: feelConverter.ConvertFrom(tempCelsiusUnit).ToString());
            }
            else
            {
                TemperatureFahrenheit tempFahrenheitUnit = new(dto.Temperature);
                TemperatureCelsius tempCelsiusUnit = tempConverter.ConvertBack(tempFahrenheitUnit);

                forecast = new(
                    date: utcConverter.ConvertFrom(dto.Date.ToDateTime(default)),  // NOTE: Ensures to always store time in the database in UTC format
                    tempC: tempCelsiusUnit.Value,
                    tempF: dto.Temperature,
                    description: feelConverter.ConvertFrom(tempCelsiusUnit).ToString());
            }

            // Validation #2
            ForecastValidator forecastValidator = this._serviceResolver.Resolve<ForecastValidator>();
            ValidatorResponse forecastValidationResult = forecastValidator.Validate(forecast);

            if (forecastValidationResult.IsInvalid)
            {
                return QueryCommandResult.Failure(forecastValidationResult.Message);
            }

            // Command
            AddForecastCommand command = this._serviceResolver.Resolve<AddForecastCommand>();
            
            return await command.ExecuteAsync(forecast, cancellationToken);
        }
    }
}
