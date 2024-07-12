using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Repository;
using WeatherForecastApp.Domain.Models;

namespace WeatherForecastApp.Persistence.Context
{
    /// <summary>
    /// <inheritdoc cref="IRepositoryContext{IRepository}"/>
    /// Implemented as a <see cref="WeatherForecastEntity"/> SQL database.
    /// </summary>
    /// <seealso cref="DbContext"/>
    /// <seealso cref="IRepositoryContext{TRepository}"/>
    public sealed class WeatherForecastContext : DbContext, IRepositoryContext<DbSet<WeatherForecastEntity>>
    {
        /// <inheritdoc cref="IRepositoryContext{TRepository}.Entities"/>
        public DbSet<WeatherForecastEntity> Entities { get; set; } = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastContext"/> class.
        /// </summary>
        public WeatherForecastContext(DbContextOptions<WeatherForecastContext> options)
            : base(options)
        {
        }

        /// <inheritdoc cref="IRepositoryContext{TRepository}.SaveChangesAsync(CancellationToken?)"/>
        public async Task<int> SaveChangesAsync(CancellationToken? cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
        }
    }
}
