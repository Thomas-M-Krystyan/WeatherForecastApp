using System;
using WeatherForecastApp.Domain.Converters.Interfaces;

namespace WeatherForecastApp.Domain.Converters
{
    /// <inheritdoc cref="IUnitConverter{TUnitA, TUnitB}"/>
    public sealed class DateTimeConverterLocalUtc : IUnitConverter<DateTime, DateTime>
    {
        /// <summary>
        /// Converts from local <see cref="DateTime"/> to <see cref="DateTime"/> in UTC format.
        /// </summary>
        public DateTime ConvertFrom(DateTime localTime)
        {
            return localTime.Kind != DateTimeKind.Utc
                ? localTime.ToUniversalTime()
                : localTime;
        }

        /// <summary>
        /// Converts from <see cref="DateTime"/> in UTC format to local <see cref="DateTime"/>.
        /// </summary>
        public DateTime ConvertBack(DateTime utcTime)
        {
            return utcTime.Kind == DateTimeKind.Utc
                ? TimeZoneInfo.ConvertTimeFromUtc(utcTime, TimeZoneInfo.Local)
                : utcTime;
        }
    }
}
