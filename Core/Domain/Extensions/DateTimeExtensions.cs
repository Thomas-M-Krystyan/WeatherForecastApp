using System;

namespace WeatherForecastApp.Domain.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/> struct.
    /// </summary>
    internal static class DateTimeExtensions
    {
        /// <summary>
        /// A method for older frameworks (.NET Framework, .NET Standard) to convert <see cref="DateTime"/> into a substitute of DateOnly type.
        /// </summary>
        /// <param name="dateTime">The dateTime time to be trimmed.</param>
        /// <returns>
        /// <see cref="DateTime"/> without time component.
        /// </returns>
        internal static DateTime ToDateOnly(this DateTime dateTime)
            => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
    }
}
