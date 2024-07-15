using System;
using WeatherForecastApp.Domain.Constants;
using WeatherForecastApp.Domain.Models;
using WeatherForecastApp.Domain.Properties;
using WeatherForecastApp.Domain.Respones;
using WeatherForecastApp.Domain.Validators.Interfaces;

namespace WeatherForecastApp.Domain.Validators
{
    /// <inheritdoc cref="IValidator{TModel}"/>
    public sealed class ForecastValidator : IValidator<WeatherForecast>
    {
        /// <inheritdoc cref="IValidator{TModel}.Validate(TModel)"/>
        public ValidatorResponse Validate(WeatherForecast model)
        {
            #pragma warning disable IDE0046  // Converting to conditional expression would decrease code readability
            if (model.DateTime.Kind != DateTimeKind.Utc)
            {
                return ValidatorResponse.Invalid(Resources.Validation_Failure_DateTimeLocal);
            }

            if (model.DateTime < DateTime.UtcNow)
            {
                return ValidatorResponse.Invalid(Resources.Validation_Failure_DateTimePast);
            }

            if (model.TempCelsius.Value < CommonValues.Database.MinAllowedTemp)
            {
                return ValidatorResponse.Invalid(Resources.Validation_Failure_TemperatureTooLow);
            }

            if (model.TempCelsius.Value > CommonValues.Database.MaxAllowedTemp)
            {
                return ValidatorResponse.Invalid(Resources.Validation_Failure_TemperatureTooHigh);
            }

            if (string.IsNullOrWhiteSpace(model.Description))
            {
                return ValidatorResponse.Invalid(Resources.Validation_Faiure_DescriptionMissing);
            }

            return ValidatorResponse.Valid();
            #pragma warning restore IDE0046
        }
    }
}
