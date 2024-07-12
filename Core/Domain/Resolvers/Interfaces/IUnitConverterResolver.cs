using WeatherForecastApp.Domain.Converters.Interfaces;
using WeatherForecastApp.Domain.Models.Units.Interfaces;

namespace WeatherForecastApp.Domain.Resolvers.Interfaces
{
    /// <summary>
    /// Resolver of <see cref="IUnitConverter{TUnitA, TUnitB}"/> services.
    /// </summary>
    public interface IUnitConverterResolver
    {
        /// <summary>
        /// Resolves a converter with the given types of <see cref="IUnit{TValue}"/>.
        /// </summary>
        /// <typeparam name="TUnitA">The first type of the <see cref="IUnit{TValue}"/>.</typeparam>
        /// <typeparam name="TUnitB">The second type of the <see cref="IUnit{TValue}"/>.</typeparam>
        public IUnitConverter<TUnitA, TUnitB> Resolve<TUnitA, TUnitB>();
    }
}
