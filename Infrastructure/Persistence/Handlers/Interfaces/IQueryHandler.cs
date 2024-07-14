using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Persistence.Commands.Base;
using WeatherForecastApp.Persistence.Entities.Interfaces;
using WeatherForecastApp.Persistence.Responses;

namespace WeatherForecastApp.Persistence.Handlers.Interfaces
{
    /// <summary>
    /// A generic query handler performing atomic operations associated with
    /// <typeparamref name="TQueryCommand"/> and manging required dependencies.
    /// </summary>
    /// <typeparam name="TQueryCommand">The type of the query command.</typeparam>
    /// <typeparam name="TEntity">The type of the respository entity.</typeparam>
    /// <typeparam name="TModel">The type of the DTO model.</typeparam>
    public interface IQueryHandler<TQueryCommand, TEntity, TModel>
        where TQueryCommand : QueryCommand<TEntity, TModel>
        where TEntity : class, IRepositoryEntity
        where TModel : struct
    {
        /// <summary>
        /// Resolves and executes the <typeparamref name="TQueryCommand"/>.
        /// </summary>
        /// <param name="dto">The Data Transfer Object (DTO) to be consumed.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The response from the executed <typeparamref name="TQueryCommand"/> operation.
        /// </returns>
        public Task<QueryCommandResult> HandleAsync(TModel dto, CancellationToken cancellationToken);
    }
}
