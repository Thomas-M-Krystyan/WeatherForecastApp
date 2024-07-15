namespace WeatherForecastApp.Domain.Constants
{
    /// <summary>
    /// Common, compile-time values used accross the solution.
    /// </summary>
    public static class CommonValues
    {
        /// <summary>
        /// The configuration keys from appsettings.json.
        /// </summary>
        public static class Settings
        {
            public const string DefaultConnectionString = "DefaultConnection";
        }

        /// <summary>
        /// Common values used by a database contexts, entities, and validation.
        /// </summary>
        public static class Database
        {
            public const float MinAllowedTemp = -60.0f;
            public const float MaxAllowedTemp =  60.0f;
            public const short MaxAllowedTextLength = 256;
        }

        /// <summary>
        /// Common values related to request attributes (protocol, base URL, body, content type...)
        /// </summary>
        public static class Request
        {
            public const string ContentType = "application/json";
        }
    }
}
