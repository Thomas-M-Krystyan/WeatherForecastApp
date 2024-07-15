using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Commands;
using WeatherForecastApp.Application.Entities;
using WeatherForecastApp.Application.Responses;

namespace WeatherForecastApp.Application.Handlers
{
    /// <summary>
    /// A generic query handler performing atomic operations associated with commands.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity model.</typeparam>
    /// <typeparam name="TModel">The type of the domain model.</typeparam>
    /// <typeparam name="TDto">The type of the DTO model.</typeparam>
    /// <remarks>
    /// NOTE: The idea behind this structure is to implement mediator design pattern or mimic MediatR library - but without
    ///       introducing a heavy, unnecessary third-party dependencies and losing control over the infratructure workflow.
    /// </remarks>
    public interface ICommandHandler<TEntity, TModel, TDto>
        where TEntity : class, IRepositoryEntity
        where TModel : struct
        where TDto : struct
    {
        /// <summary>
        /// Resolves and executes the <typeparamref name="TCommand"/>.
        /// </summary>
        /// <param name="dto">The Data Transfer Object (DTO) to be passed.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <returns>
        /// The response from the executed <typeparamref name="TCommand"/> operation.
        /// </returns>
        public Task<QueryCommandResult> HandleAsync<TCommand>(TDto dto, CancellationToken cancellationToken)
            where TCommand : class, IQueryCommand<TEntity, TModel>;
    }
}
