using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WeatherForecastApp.WebApi.Enums;

namespace WeatherForecastApp.WebApi.Models.DTOs
{
    /// <summary>
    /// A generic weather forecast Data Transfer Object (DTO) accepting user inputs.
    /// </summary>
    public struct WeatherForecastDto
    {
        /// <summary>
        /// The date for which the weather forecast is made.
        /// </summary>
        [JsonRequired]
        [JsonPropertyName(nameof(Date))]
        [JsonPropertyOrder(0)]
        public DateOnly Date { get; set; }

        /// <summary>
        /// The value of the temperature.
        /// </summary>
        [Required]
        [JsonPropertyName(nameof(Temperature))]
        [JsonPropertyOrder(1)]
        public float Temperature { get; set; }

        /// <summary>
        /// The scale of the temperature (°C, °F).
        /// </summary>
        [Required]
        [JsonPropertyName(nameof(Scale))]
        [JsonPropertyOrder(2)]
        public TemperatureScales Scale { get; set; }
    }
}
