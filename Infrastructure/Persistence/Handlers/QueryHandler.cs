using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Repository;
using WeatherForecastApp.Domain.Resolvers.Interfaces;
using WeatherForecastApp.Persistence.Commands.Base;
using WeatherForecastApp.Persistence.Entities.Interfaces;
using WeatherForecastApp.Persistence.Handlers.Interfaces;
using WeatherForecastApp.Persistence.Responses;

namespace WeatherForecastApp.Persistence.Handlers
{
    /// <inheritdoc cref="IQueryHandler{TQueryCommand, TEntity, TModel}"/>
    public sealed class QueryHandler<TEntity, TModel> : IQueryHandler<QueryCommand<TEntity, TModel>, TEntity, TModel>
        where TEntity : class, IRepositoryEntity
        where TModel : struct
    {
        private readonly IRepositoryContext<DbSet<TEntity>> _repositoryContext;
        private readonly IServiceResolver _serviceResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="IQueryHandler{TCommand, TEntity, TModel}"/> class.
        /// </summary>
        public QueryHandler(IRepositoryContext<DbSet<TEntity>> repositoryContext, IServiceResolver serviceResolver)
        {
            this._repositoryContext = repositoryContext;
            this._serviceResolver = serviceResolver;
        }

        /// <inheritdoc cref="IQueryHandler{TQueryCommand, TEntity, TModel}.HandleAsync(TModel, CancellationToken)"/>
        public async Task<QueryCommandResult> HandleAsync(TModel dto, CancellationToken cancellationToken)
        {
            QueryCommand<TEntity, TModel> queryCommand = this._serviceResolver.Resolve<QueryCommand<TEntity, TModel>>();

            return await queryCommand.ExecuteAsync(this._repositoryContext, dto, cancellationToken);
        }
    }
}
