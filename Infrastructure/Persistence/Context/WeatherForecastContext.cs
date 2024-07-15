using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Repository;
using WeatherForecastApp.Application.Responses;
using WeatherForecastApp.Domain.Extensions;
using WeatherForecastApp.Domain.Models;

namespace WeatherForecastApp.Persistence.Context
{
    /// <summary>
    /// <inheritdoc cref="IRepositoryContext{IRepository}"/>
    /// Implemented as a <see cref="WeatherForecastEntity"/> for a dedicated table in SQL database.
    /// </summary>
    public sealed class WeatherForecastContext : DbContext, IRepositoryContext<DbSet<WeatherForecastEntity>>
    {
        private readonly ILogger<WeatherForecastContext> _logger;

        /// <inheritdoc cref="IRepositoryContext{TRepository}.Entities"/>
        public DbSet<WeatherForecastEntity> Entities { get; set; } = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastContext"/> class.
        /// </summary>
        public WeatherForecastContext(DbContextOptions<WeatherForecastContext> options, ILogger<WeatherForecastContext> logger)
            : base(options)
        {
            this._logger = logger;
        }

        /// <inheritdoc cref="IRepositoryContext{TRepository}.SaveChangesAsync(CancellationToken?)"/>
        public async Task<QueryResult> SaveChangesAsync(CancellationToken? cancellationToken)
        {
            try
            {
                int changesCount = await base.SaveChangesAsync(cancellationToken ?? CancellationToken.None);

                return changesCount > 0
                    ? QueryResult.Success(changesCount)
                    : QueryResult.Failure();
            }
            catch (Exception exception)
            {
                this._logger.LogDetailed(exception);

                return QueryResult.Failure();
            }
        }
    }
}
