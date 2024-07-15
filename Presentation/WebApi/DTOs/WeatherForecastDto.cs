using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherForecastApp.WebApi.DTOs
{
    /// <summary>
    /// A generic weather forecast Data Transfer Object (DTO) accepting user inputs.
    /// </summary>
    public struct WeatherForecastDto
    {
        /// <summary>
        /// The date and time for which the weather forecast is made.
        /// </summary>
        [Required]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// The temperature in scale of user choice (°C, °F, °K).
        /// </summary>
        [Required]
        public float Temperature { get; set; }
    }
}
