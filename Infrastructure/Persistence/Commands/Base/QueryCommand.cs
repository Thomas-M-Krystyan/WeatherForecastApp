using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Repository;
using WeatherForecastApp.Persistence.Entities.Interfaces;
using WeatherForecastApp.Persistence.Responses;

namespace WeatherForecastApp.Persistence.Commands.Base
{
    /// <summary>
    /// The generic type of query operation command.
    /// </summary>
    /// <typeparam name="TEntity">The type of the respository entity.</typeparam>
    /// <typeparam name="TModel">The type of the DTO model.</typeparam>
    public abstract class QueryCommand<TEntity, TModel>
        where TEntity : class, IRepositoryEntity
        where TModel : struct
    {
        /// <summary>
        /// Process a specific operation implemented by the command.
        /// </summary>
        /// <param name="dto">The Data Transfer Object (DTO) to be consumed.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The response from the executed <see cref="QueryCommand{TEntity, TModel}"/> operation.
        /// </returns>
        public abstract Task<QueryCommandResult> ExecuteAsync(
            IRepositoryContext<DbSet<TEntity>> repositoryContext, TModel dto, CancellationToken cancellationToken = default);
    }
}
