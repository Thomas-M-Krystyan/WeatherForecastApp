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
        [JsonPropertyName(nameof(DateTime))]
        [JsonPropertyOrder(0)]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// The temperature in scale of user choice (°C, °F, °K).
        /// </summary>
        [Required]
        [JsonPropertyName(nameof(Temperature))]
        [JsonPropertyOrder(1)]
        public float Temperature { get; set; }
    }
}
