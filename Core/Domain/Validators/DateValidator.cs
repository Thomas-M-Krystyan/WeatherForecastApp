﻿using System;
using WeatherForecastApp.Domain.Properties;
using WeatherForecastApp.Domain.Respones;
using WeatherForecastApp.Domain.Validators.Interfaces;

namespace WeatherForecastApp.Domain.Validators
{
    /// <inheritdoc cref="IValidator{TModel}"/>
    public sealed class DateValidator : IValidator<DateTime>
    {
        /// <inheritdoc cref="IValidator{TModel}.Validate(TModel)"/>
        public ValidatorResponse Validate(DateTime dateTime)
        {
            #pragma warning disable IDE0046  // Converting to conditional expression would decrease code readability
            if (dateTime.Kind == DateTimeKind.Local)
            {
                return ValidatorResponse.Invalid(Resources.Validation_Failure_DateLocal);
            }

            if (dateTime < DateTime.UtcNow)
            {
                return ValidatorResponse.Invalid(Resources.Validation_Failure_DatePast);
            }

            return ValidatorResponse.Valid();
            #pragma warning restore IDE0046
        }
    }
}
