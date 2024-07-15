using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeatherForecastApp.Application.Entities;
using WeatherForecastApp.Domain.Constants;

namespace WeatherForecastApp.Domain.Models
{
    /// <summary>
    /// The SQL database entity corresponding to <see cref="WeatherForecast"/> domain model.
    /// </summary>
    [Table(nameof(WeatherForecast))]
    public sealed record WeatherForecastEntity : IRepositoryEntity
    {
        /// <inheritdoc cref="WeatherForecast.DateTime"/>
        [Key]
        public DateOnly Date { get; set; }

        /// <inheritdoc cref="WeatherForecast.TempCelsius"/>
        [Required]
        [Range(CommonValues.Database.MinAllowedTemp, CommonValues.Database.MaxAllowedTemp)]
        public float TempCelsius { get; set; }

        // NOTE: To save some space in the database only one temperature unit is
        // required. The n-other unit(s) can be obtained through unit conversion

        /// <inheritdoc cref="WeatherForecast.Description"/>
        [Required]
        [StringLength(CommonValues.Database.MaxAllowedTextLength)]
        public string Description { get; set; } = string.Empty;
    }
}
