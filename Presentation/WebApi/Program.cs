using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using WeatherForecastApp.Application.Repository;
using WeatherForecastApp.Domain.Constants;
using WeatherForecastApp.Domain.Models;
using WeatherForecastApp.Domain.Resolvers;
using WeatherForecastApp.Domain.Resolvers.Interfaces;
using WeatherForecastApp.Persistence.Context;
using WeatherForecastApp.WebApi.Constants;
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
            // API endpoints
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // MS SQL Server
            builder.Services.AddDbContext<WeatherForecastContext>(options =>
            {
                options.UseSqlServer(
                    connectionString: Environment.GetEnvironmentVariable(CommonValues.EnvironmentVariables.ConnectionString));
            });

            // Swagger UI
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

            // Version of the application
            builder.Services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(ApiCommonValues.Version.Default);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            return builder;
        }

        private static WebApplicationBuilder RegisterInternalServices(this WebApplicationBuilder builder)
        {
            // Repository
            builder.Services.AddScoped<IRepositoryContext<DbSet<WeatherForecastEntity>>, WeatherForecastContext>();

            // Resolvers
            builder.Services.AddSingleton<IServiceHandler, ServiceHandler>();
            builder.Services.AddSingleton<IServiceResolver, ServiceResolver>();

            return builder;
        }

        private static WebApplication ConfigureHttpPipeline(this WebApplicationBuilder builder)
        {
            WebApplication app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(option =>
                option.SwaggerEndpoint($"/swagger/{Resource.Swagger_Version}/swagger.json", nameof(WeatherForecastApp)));

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
