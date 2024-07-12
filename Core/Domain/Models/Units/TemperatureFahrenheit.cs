using WeatherForecastApp.Domain.Interfaces.Units;

namespace WeatherForecastApp.Domain.Models.Units
{
    /// <summary>
    /// <inheritdoc cref="IUnit{TValue}"/>
    /// <para>
    /// Implementation of temperature in Fahrenheit degrees.
    /// </para>
    /// </summary>
    public readonly struct TemperatureFahrenheit : IUnit<float>
    {
        /// <inheritdoc cref="IUnit{TValue}.Name"/>
        public static string Name => "Fahrenheit";

        /// <inheritdoc cref="IUnit{TValue}.Symbol"/>
        public static string Symbol => "F°";

        /// <inheritdoc cref="IUnit{TValue}.Value"/>
        public float Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemperatureFahrenheit"/> struct.
        /// </summary>
        /// <param name="value"><inheritdoc cref="Value" path="/summary"/></param>
        public TemperatureFahrenheit(float value)
        {
            this.Value = value;
        }
    }
}
