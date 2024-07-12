using System.Data.Entity;
using System.Threading.Tasks;
using WeatherForecastApp.Domain.Entities.Models;

namespace WeatherForecastApp.Application.Entities.Interfaces
{
    public interface IDbContext
    {
        public DbSet<WeatherForecast> Products { get; set; }

        public Task<int> SaveChanges();
    }
}
