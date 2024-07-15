using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Entities;
using WeatherForecastApp.Application.Responses;

namespace WeatherForecastApp.Application.Commands
{
    /// <summary>
    /// The generic type of query operation command.
    /// </summary>
    /// <typeparam name="TEntity">The type of the respository entity.</typeparam>
    /// <typeparam name="TModel">The type of the DTO model.</typeparam>
    /// <remarks>
    /// NOTE: The idea behind this structure is to implement command design pattern
    ///       and inverse the dependency how the business logic is handled / located.
    /// </remarks>
    public interface IQueryCommand<TEntity, TModel>
        where TEntity : class, IRepositoryEntity
        where TModel : struct
    {
        /// <summary>
        /// Process a specific operation implemented by the command.
        /// </summary>
        /// <param name="model">The data model to be consumed.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The response from the executed <see cref="IQueryCommand{TEntity, TModel}"/> operation.
        /// </returns>
        public abstract Task<QueryCommandResult> ExecuteAsync(TModel model, CancellationToken cancellationToken = default);
    }
}
