using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using WeatherForecastApp.Domain.Converters.Interfaces;
using WeatherForecastApp.Domain.Resolvers.Interfaces;

namespace WeatherForecastApp.Domain.Resolvers
{
    /// <inheritdoc cref="IUnitConverterResolver"/>
    public sealed class UnitConverterResolver : IUnitConverterResolver
    {
        private readonly IDictionary<string, object> _cachedConverters = new ConcurrentDictionary<string, object>();

        /// <inheritdoc cref="IUnitConverterResolver.Resolve{TUnitA, TUnitB}"/>
        public IUnitConverter<TUnitA, TUnitB> Resolve<TUnitA, TUnitB>()
        {
            if (this._cachedConverters.TryGetValue(nameof(IUnitConverter<TUnitA, TUnitB>), out object? cachedConverter))
            {
                return (IUnitConverter<TUnitA, TUnitB>)cachedConverter;
            }

            IUnitConverter<TUnitA, TUnitB> createdConverter = Activator.CreateInstance<IUnitConverter<TUnitA, TUnitB>>();

            this._cachedConverters.TryAdd(nameof(IUnitConverter<TUnitA, TUnitB>), createdConverter);

            return createdConverter;
        }
    }
}
