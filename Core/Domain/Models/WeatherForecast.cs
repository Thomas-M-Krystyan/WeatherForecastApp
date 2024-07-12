using System;

namespace WeatherForecastApp.Domain.Models
{
    /// <summary>
    /// The basic weather forecast domain model.
    /// </summary>
    public readonly struct WeatherForecast
    {
        /// <summary>
        /// The date when the weather forecast was published.
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// The temperature in C°.
        /// </summary>
        public float TemperatureInC { get; }

        /// <summary>
        /// The temperature in F°.
        /// </summary>
        public float TemperatureInF { get; }

        /// <summary>
        /// The human-friendly weather forecast description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecast"/> struct.
        /// </summary>
        /// <param name="date"><inheritdoc cref="DateTime" path="/summary"/></param>
        /// <param name="tempC"><inheritdoc cref="TemperatureInC" path="/summary"/></param>
        /// <param name="tempF"><inheritdoc cref="TemperatureInF" path="/summary"/></param>
        /// <param name="description"><inheritdoc cref="Description" path="/summary"/></param>
        public WeatherForecast(DateTime date, float tempC, float tempF, string description)
        {
            this.DateTime = date;
            this.TemperatureInC = tempC;
            this.TemperatureInF = tempF;
            this.Description = description;
        }
    }
}
