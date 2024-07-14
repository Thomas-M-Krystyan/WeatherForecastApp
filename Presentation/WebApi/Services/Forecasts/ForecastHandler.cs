using Microsoft.EntityFrameworkCore;
using WeatherForecastApp.Application.Repository;
using WeatherForecastApp.Domain.Models;

namespace WeatherForecastApp.WebApi.Services.Forecasts
{
    internal sealed class ForecastHandler
    {
        private readonly IRepositoryContext<DbSet<WeatherForecastEntity>> _repositoryDbContext;

        public ForecastHandler(IRepositoryContext<DbSet<WeatherForecastEntity>> repositoryDbContext)
        {
            this._repositoryDbContext = repositoryDbContext;
        }

        internal void Handle()
        {
        }
    }
}
