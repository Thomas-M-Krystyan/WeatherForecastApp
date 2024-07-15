namespace WeatherForecastApp.Domain.Resolvers.Interfaces
{
    /// <summary>
    /// Resolves given services in real-time (using a couple of built-in strategies)
    /// <list type="bullet">
    ///   <item>To avoid passing multiple dependencies in constructors and method signatures (but still using DI container)</item>
    ///   <item>To spare the additional effort on registering multiple smaller services at startup - call them on demand instead</item>
    ///   <item>To provide another layer of protection in case of services that were forgotten to be registered</item>
    /// </list>
    /// TODO: To be improved is the possibility of resolving services with different lifetime scopes, not only singletons.
    /// </summary>
    /// <remarks>
    /// NOTE: Services failed to restore and re-created by the <see cref="IServiceResolver"/> will be cached (they would act as singletons).
    /// </remarks>
    public interface IServiceResolver
    {
        /// <summary>
        /// Tries to resolve a given service on demand.
        /// <para>
        /// If the service is not yet registered it will be instantiated and cached.
        /// </para>
        /// </summary>
        /// <typeparam name="TService">The service to be resolved.</typeparam>
        public TService Resolve<TService>()
            where TService : class;
    }
}
