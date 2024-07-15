using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WeatherForecastApp.Domain.Resolvers.Interfaces
{
    /// <summary>
    /// Performs dedicated <see cref="IServiceResolver"/> operations.
    /// </summary>
    public interface IServiceHandler
    {
        private IDictionary<string, object> CachedServices
            => new ConcurrentDictionary<string /* service name */, object /* instance */>();

        /// <summary>
        /// Tries to retrieve a given service from the internal <see cref="IServiceHandler"/> cache.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="cachedService">The service retrieved from the cache.</param>
        /// <returns>
        ///   <see langword="true"/> if the service was retrieved from the cache; otherwise, <see langword="false"/>.
        /// </returns>
        public bool GetCachedService<TService>(out object cachedService)
            => this.CachedServices.TryGetValue(nameof(TService), out cachedService);

        /// <summary>
        /// Creates the given service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>
        ///   A new instance of the desired service.
        /// </returns>
        public TService CreateService<TService>()
            where TService : class
            => Activator.CreateInstance<TService>();  // TODO: Handle objects without parameterless constructors

        /// <summary>
        /// Caches the given service in the internal <see cref="IServiceHandler"/> cache.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="service">The service to be cached.</param>
        public void CacheService<TService>(TService service)
            where TService : class
            => this.CachedServices.TryAdd(nameof(TService), service);
    }
}
