﻿using Microsoft.EntityFrameworkCore;
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
    /// Implemented as a <see cref="WeatherForecastEntity"/> SQL database.
    /// </summary>
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
        public async Task<QueryResult> SaveChangesAsync(CancellationToken? cancellationToken)
        {
            int changesCount = await base.SaveChangesAsync(cancellationToken ?? CancellationToken.None);

            return new QueryResult(changesCount > 0, changesCount);
        }

        /// <inheritdoc cref="DbContext.Dispose()"/>
        void IDisposable.Dispose()
        {
            base.Dispose();
        }
    }
}
