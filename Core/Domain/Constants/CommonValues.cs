namespace WeatherForecastApp.Domain.Constants
{
    /// <summary>
    /// Common compile-time values used accross the solution.
    /// </summary>
    public static class CommonValues
    {
        public static class Database
        {
            public const float MinAllowedTemp = -60.0f;
            public const float MaxAllowedTemp =  60.0f;
            public const short MaxAllowedTextLength = 256;
        }
    }
}
