using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Repository;
using WeatherForecastApp.Application.Responses;
using WeatherForecastApp.Domain.Models;

namespace WeatherForecastApp.Persistence.Context
{
    /// <summary>
    /// <inheritdoc cref="IRepositoryContext{IRepository}"/>
    /// Implemented as a <see cref="WeatherForecastEntity"/> for a dedicated table in SQL database.
    /// </summary>
    public sealed class WeatherForecastContext : DbContext, IRepositoryContext<DbSet<WeatherForecastEntity>>
    {
        private bool _isDisposed;

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
        public async Task<RepositoryResult> SaveChangesAsync(CancellationToken? cancellationToken)
        {
            int changesCount = await base.SaveChangesAsync(cancellationToken ?? CancellationToken.None);

            return new RepositoryResult(changesCount > 0, changesCount);
        }

        /// <inheritdoc cref="DbContext.Dispose()"/>
        void IDisposable.Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this._isDisposed && disposing)
            {
                base.Dispose();

                this._isDisposed = true;
            }
        }
    }
}
