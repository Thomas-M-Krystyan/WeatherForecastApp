namespace WeatherForecastApp.Domain.Models.Units.Interfaces
{
    /// <summary>
    /// The model representing a base unit of measurement.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface IUnit<TValue>
    {
        /// <summary>
        /// The name of the unit.
        /// </summary>
        /// <remarks>
        /// This value is set once and persisted during the application lifetime.
        /// </remarks>
        public static string Name { get; } = string.Empty;

        /// <summary>
        /// The symbol of the unit.
        /// </summary>
        /// <remarks>
        /// This value is set once and persisted during the application lifetime.
        /// </remarks>
        public static string Symbol { get; } = string.Empty;

        /// <summary>
        /// The current value of the unit.
        /// </summary>
        public TValue Value { get; }
    }
}
