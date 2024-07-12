﻿using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherForecastApp.Application.Repository
{
    /// <summary>
    /// The generic representation of a repository context.
    /// <para>
    /// By a <typeparamref name="TRepository"/> it's meant: SQL database, spreadsheet / CSV file, text file, etc.
    /// </para>
    /// </summary>
    /// <typeparam name="TRepository">The type of a repository.</typeparam>
    public interface IRepositoryContext<TRepository>
        where TRepository : class, IQueryable, IEnumerable
    {
        /// <summary>
        /// Gets or sets the entities from a given repository.
        /// </summary>
        public TRepository Entities { get; set; }

        /// <summary>
        /// Saves the changes to a given repository asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   The number of affected rows.
        /// </returns>
        public Task<int> SaveChangesAsync(CancellationToken? cancellationToken = default);
    }
}
