using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherForecastApp.Application.Entities.Interfaces
{
    /// <summary>
    /// The generic representation of a database context.
    /// </summary>
    /// <typeparam name="TEntity">The type of a database entity.</typeparam>
    public interface IDomainContext<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets or sets the entities from a database.
        /// </summary>
        public DbSet<TEntity> Entities { get; set; }

        /// <summary>
        /// Saves the changes to a database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The number of affected rows.
        /// </returns>
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
