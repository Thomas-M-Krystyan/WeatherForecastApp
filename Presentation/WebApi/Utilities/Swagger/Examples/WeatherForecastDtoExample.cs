using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;
using WeatherForecastApp.WebApi.Models.DTOs;

namespace WeatherForecastApp.WebApi.Utilities.Swagger.Examples
{
    /// <inheritdoc cref="IExamplesProvider{T}"/>
    [ExcludeFromCodeCoverage]
    internal sealed class WeatherForecastDtoExample : IExamplesProvider<WeatherForecastDto>
    {
        /// <inheritdoc cref="IExamplesProvider{T}.GetExamples()"/>
        public WeatherForecastDto GetExamples()
        {
            return new WeatherForecastDto
            {
                DateTime = System.DateTime.UtcNow,
                Temperature = 25.0f,
                Scale = TemperatureScales.Celsius
            };
        }
    }
}
