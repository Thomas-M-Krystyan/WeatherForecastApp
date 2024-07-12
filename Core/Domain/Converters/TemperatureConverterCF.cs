using WeatherForecastApp.Domain.Converters.Interfaces;
using WeatherForecastApp.Domain.Models.Units;

namespace WeatherForecastApp.Domain.Converters
{
    /// <summary>
    /// <inheritdoc cref="IUnitConverter{TUnitA, TUnitB}"/>
    /// <para>
    /// Implements a converter between <see cref="TemperatureCelsius"/> and <see cref="TemperatureFahrenheit"/>.
    /// </para>
    /// </summary>
    /// <remarks>Source: https://www.thoughtco.com/celcius-to-farenheit-formula-609227</remarks>
    public sealed class TemperatureConverterCF : IUnitConverter<TemperatureCelsius, TemperatureFahrenheit>
    {
        private static float CelsiusToFahrenheit(float tempC) => tempC * 9 / 5 + 32;

        private static float FahrenheitToCelsius(float tempF) => tempF - 32 * 5 / 9;

        /// <inheritdoc cref="IUnitConverter{TUnitA, TUnitB}.ConvertTo(TUnitA)"/>
        public TemperatureFahrenheit ConvertTo(TemperatureCelsius unit)
        {
            return new TemperatureFahrenheit(CelsiusToFahrenheit(unit.Value));
        }

        /// <inheritdoc cref="IUnitConverter{TUnitA, TUnitB}.ConvertBack(TUnitB)"/>
        public TemperatureCelsius ConvertBack(TemperatureFahrenheit unit)
        {
            return new TemperatureCelsius(FahrenheitToCelsius(unit.Value));
        }
    }
}
