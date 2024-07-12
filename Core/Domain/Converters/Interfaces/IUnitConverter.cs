using WeatherForecastApp.Domain.Models.Units.Interfaces;

namespace WeatherForecastApp.Domain.Converters.Interfaces
{
    /// <summary>
    /// The generic <see cref="IUnit{TValue}"/> converter, performing transformations in both directions.
    /// </summary>
    /// <typeparam name="TUnitA">The first type of the <see cref="IUnit{TValue}"/>.</typeparam>
    /// <typeparam name="TUnitB">The second type of the <see cref="IUnit{TValue}"/>.</typeparam>
    public interface IUnitConverter<TUnitA, TUnitB>
    {
        /// <summary>
        /// Converts <see cref="TUnitA"/> into <see cref="TUnitB"/>.
        /// </summary>
        public TUnitB ConvertTo(TUnitA unit);

        /// <summary>
        /// Converts <see cref="TUnitB"/> into <see cref="TUnitA"/>.
        /// </summary>
        public TUnitA ConvertBack(TUnitB unit);
    }
}
