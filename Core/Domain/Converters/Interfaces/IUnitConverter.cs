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
        /// Converts from <typeparamref name="TUnitA"/> to <typeparamref name="TUnitB"/>.
        /// </summary>
        public TUnitB ConvertFrom(TUnitA unit);

        /// <summary>
        /// Converts <typeparamref name="TUnitB"/> back to <typeparamref name="TUnitA"/>.
        /// </summary>
        public TUnitA ConvertBack(TUnitB unit);
    }
}
