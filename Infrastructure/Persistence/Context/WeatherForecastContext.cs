using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Entities.Interfaces;
using WeatherForecastApp.Domain.Entities.Models;

namespace WeatherForecastApp.Persistence.Context
{
    /// <summary>
    /// The weather forecast database context.
    /// </summary>
    /// <seealso cref="DbContext" />
    /// <seealso cref="IDomainContext{TEntity}" />
    public sealed class WeatherForecastContext : DbContext, IDomainContext<WeatherForecast>
    {
        /// <inheritdoc cref="IDomainContext{TEntity}.Entities"/>
        public System.Data.Entity.DbSet<WeatherForecast> Entities { get; set; } = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastContext"/> class.
        /// </summary>
        public WeatherForecastContext(DbContextOptions<WeatherForecastContext> options)
            : base(options)
        {
        }

        /// <inheritdoc cref="IDomainContext{TEntity}.SaveChangesAsync(CancellationToken)"/>
        async Task<int> IDomainContext<WeatherForecast>.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await SaveChangesAsync(cancellationToken);
        }
    }
}
