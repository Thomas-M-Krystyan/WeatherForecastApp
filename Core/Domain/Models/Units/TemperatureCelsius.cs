using WeatherForecastApp.Domain.Constants;
using WeatherForecastApp.Domain.Interfaces.Units;

namespace WeatherForecastApp.Domain.Models.Units
{
    /// <summary>
    /// <inheritdoc cref="IUnit{TValue}"/>
    /// <para>
    /// Implementation of temperature in Celsius degrees.
    /// </para>
    /// </summary>
    public readonly struct TemperatureCelsius : IUnit<float>
    {
        /// <inheritdoc cref="IUnit{TValue}.Name"/>
        public static string Name => "Celsius";

        /// <inheritdoc cref="IUnit{TValue}.Symbol"/>
        public static string Symbol => "C°";

        /// <inheritdoc cref="IUnit{TValue}.Value"/>
        public float Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemperatureCelsius"/> struct.
        /// </summary>
        /// <param name="value"><inheritdoc cref="Value" path="/summary"/></param>
        public TemperatureCelsius(float value)
        {
            this.Value = value;
        }
    }
}
