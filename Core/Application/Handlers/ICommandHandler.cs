using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Responses;

namespace WeatherForecastApp.Application.Handlers
{
    /// <summary>
    /// A generic query handler setting up and performing atomic operations associated with commands.
    /// </summary>
    /// <typeparam name="TData">The type of the DTO model.</typeparam>
    /// <remarks>
    /// NOTE: The idea behind this structure is to implement mediator design pattern or mimic MediatR library - but without
    ///       introducing a heavy, unnecessary third-party dependencies and losing control over the infratructure workflow.
    /// </remarks>
    public interface ICommandHandler<TData>
        where TData : struct
    {
        /// <summary>
        /// Resolves and executes the dedicated command.
        /// </summary>
        /// <param name="data">The data to be passed to the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The response from the command operation.
        /// </returns>
        public Task<QueryCommandResult> HandleAsync(TData data, CancellationToken cancellationToken);
    }
}
