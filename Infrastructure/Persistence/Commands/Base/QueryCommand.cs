using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Repository;
using WeatherForecastApp.Domain.Resolvers.Interfaces;
using WeatherForecastApp.Persistence.Entities.Interfaces;
using WeatherForecastApp.WebApi.Responses;

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
        /// <inheritdoc cref="IRepositoryContext{TRepository}"/>
        protected IRepositoryContext<DbSet<TEntity>> RepositoryContext { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCommand{TEntity, TModel}"/> class.
        /// </summary>
        protected QueryCommand(IServiceResolver serviceResolver)
        {
            this.RepositoryContext = serviceResolver.Resolve<IRepositoryContext<DbSet<TEntity>>>();
        }

        /// <summary>
        /// Process a specific operation implemented by the command.
        /// </summary>
        /// <param name="dto">The Data Transfer Object (DTO) to be consumed.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The response from the executed command operation.
        /// </returns>
        public abstract Task<QueryCommandResult> ExecuteAsync(TModel dto, CancellationToken cancellationToken = default);
    }
}
