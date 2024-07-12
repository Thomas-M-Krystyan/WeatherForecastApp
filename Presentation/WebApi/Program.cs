using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using WeatherForecastApp.Application.Repository;
using WeatherForecastApp.Domain.Models;
using WeatherForecastApp.Persistence.Context;
using WeatherForecastApp.WebApi.Properties;

namespace WebApi
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            WebApplication.CreateBuilder(args)
                .RegisterExternalServices()  // .NET and other 3rd party services
                .RegisterInternalServices()  // solution-specific internal services
                .ConfigureHttpPipeline()
                .Run();
        }

        private static WebApplicationBuilder RegisterExternalServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                // Mapping XML documentation
                string applicationDirectory = AppContext.BaseDirectory;
                string applicationDocName = $"{nameof(WeatherForecastApp)}.{nameof(WebApi)}.xml";
                string applicationDocPath = Path.Combine(applicationDirectory, applicationDocName);

                options.IncludeXmlComments(applicationDocPath);
                options.SwaggerDoc(Resource.Swagger_Version, new OpenApiInfo
                {
                    Version = Resource.Swagger_Version,
                    Title = Resource.Swagger_Title,
                    Description = Resource.Swagger_Description,
                });
            });

            return builder;
        }

        private static WebApplicationBuilder RegisterInternalServices(this WebApplicationBuilder builder)
        {
            // Repository
            builder.Services.AddSingleton<IRepositoryContext<DbSet<WeatherForecastEntity>>, WeatherForecastContext>();

            return builder;
        }

        private static WebApplication ConfigureHttpPipeline(this WebApplicationBuilder builder)
        {
            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
