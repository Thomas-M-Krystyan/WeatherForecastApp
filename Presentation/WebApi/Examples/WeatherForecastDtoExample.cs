using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;
using WeatherForecastApp.WebApi.DTOs;

namespace WeatherForecastApp.WebApi.Examples
{
    /// <inheritdoc cref="IExamplesProvider{T}"/>
    [ExcludeFromCodeCoverage]
    internal readonly struct WeatherForecastDtoExample : IExamplesProvider<WeatherForecastDto>
    {
        /// <inheritdoc cref="IExamplesProvider{T}.GetExamples()"/>
        public WeatherForecastDto GetExamples()
        {
            return new WeatherForecastDto
            {
                DateTime = System.DateTime.UtcNow,
                Temperature = 25.0f
            };
        }
    }
}
