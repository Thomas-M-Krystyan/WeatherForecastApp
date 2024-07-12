using System;

namespace WeatherForecastApp.Domain.Entities.Models
{
    public sealed class WeatherForecast
    {
        public DateTime Date { get; set; }

        public float TemperatureInC { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
