using System;
using WeatherForecastApp.Domain.Resolvers.Interfaces;

namespace WeatherForecastApp.Domain.Resolvers
{
    /// <inheritdoc cref="IServiceResolver"/>
    public sealed class ServiceResolver : IServiceResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceHandler _serviceHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResolver"/> class.
        /// </summary>
        public ServiceResolver(IServiceProvider serviceProvider, IServiceHandler serviceHandler)
        {
            this._serviceProvider = serviceProvider;
            this._serviceHandler = serviceHandler;
        }

        /// <inheritdoc cref="IServiceResolver.Resolve{TService}"/>
        public TService Resolve<TService>()
            where TService : class
        {
            // Step #1: Resolves the registered service from Dependency Injection container
            object resolvedService = this._serviceProvider.GetService(typeof(TService));

            if (resolvedService is TService desiredService)
            {
                return desiredService;
            }

            // Step #2: Retrieves the service from the cache (if it was not registered before)
            if (this._serviceHandler.GetCachedService<TService>(out object cachedService))
            {
                return (TService)cachedService;
            }

            // Step #3: Creates and cache a new instance of the given service (if it was not cached before)
            TService createdService = this._serviceHandler.CreateService<TService>();

            this._serviceHandler.CacheService(cachedService);

            return createdService;
        }
    }
}
