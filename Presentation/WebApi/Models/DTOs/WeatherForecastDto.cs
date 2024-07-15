using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WeatherForecastApp.WebApi.Models.DTOs
{
    /// <summary>
    /// A generic weather forecast Data Transfer Object (DTO) accepting user inputs.
    /// </summary>
    public struct WeatherForecastDto
    {
        /// <summary>
        /// The date and time for which the weather forecast is made.
        /// </summary>
        [JsonRequired]
        [JsonInclude]
        [JsonPropertyName(nameof(DateTime))]
        [JsonPropertyOrder(0)]
        internal DateTime DateTime { get; set; }

        /// <summary>
        /// The value of the temperature.
        /// </summary>
        [Required]
        [JsonInclude]
        [JsonPropertyName(nameof(Temperature))]
        [JsonPropertyOrder(1)]
        internal float Temperature { get; set; }

        /// <summary>
        /// The scale of the temperature (°C, °F).
        /// </summary>
        [Required]
        [JsonInclude]
        [JsonPropertyName(nameof(Scale))]
        [JsonPropertyOrder(2)]
        internal TemperatureScales Scale { get; set; }
    }
}
