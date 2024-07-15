using Swashbuckle.AspNetCore.Filters;

namespace WeatherForecastApp.Persistence.Constants
{
    /// <summary>
    /// Common values for the main API project.
    /// </summary>
    internal static class ApiCommonValues
    {
        /// <summary>
        /// Version of the application.
        /// </summary>
        internal static class Version
        {
            internal const double Default = 1.0;
        }

        /// <summary>
        /// Example values used to document properties in API endpoints, or parameters in <see cref="IExamplesProvider{T}"/> for Swagger UI.
        /// </summary>
        internal static class Examples
        {
            internal const string DateOnly = "2024-07-15";
        }
    }
}
