namespace WeatherForecastApp.Domain.Resolvers.Interfaces
{
    /// <summary>
    /// Resolves given services in real-time (using a couple of built-in strategies)
    /// e.g., to spare registering them manually in Dependency Injection container or
    /// to provide an emergency fallback scenario - always returning a desired service.
    /// </summary>
    /// <remarks>
    /// NOTE: Services created by the <see cref="IServiceResolver"/> will be cached (they would act later as singletons).
    /// </remarks>
    public interface IServiceResolver
    {
        /// <summary>
        /// Tries to resolve a given service.
        /// <para>
        /// If the service is not yet registered it will be instantiated and cached.
        /// </para>
        /// </summary>
        /// <typeparam name="TService">The service to be resolved.</typeparam>
        public TService Resolve<TService>()
            where TService : class;
    }
}
