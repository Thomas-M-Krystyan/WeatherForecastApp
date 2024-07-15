using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Text.Json.Serialization;
using WeatherForecastApp.Domain.Constants;
using WeatherForecastApp.Domain.Resolvers;
using WeatherForecastApp.Domain.Resolvers.Interfaces;
using WeatherForecastApp.Domain.Validators;
using WeatherForecastApp.Persistence.Commands;
using WeatherForecastApp.Persistence.Constants;
using WeatherForecastApp.Persistence.Context;
using WeatherForecastApp.Persistence.Properties;
using WeatherForecastApp.WebApi.Handlers;
using WeatherForecastApp.WebApi.Utilities.Swagger.Examples;

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
            // Configuration appsettings.json files
            const string settingsFileName = "appsettings";
            builder.Configuration.AddJsonFile($"{settingsFileName}.json", optional: false)
                                 .AddJsonFile($"{settingsFileName}.{builder.Environment.EnvironmentName}.json", optional: true);

            // API endpoints
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));  // Display enum values as strings in Swagger UI

            builder.Services.AddEndpointsApiExplorer();

            // MS SQL Server
            builder.Services.AddDbContext<WeatherForecastContext>(options =>
            {
                options.UseSqlServer(
                    connectionString: builder.Configuration.GetConnectionString(CommonValues.Settings.DefaultConnectionString));
            });

            // Swagger UI
            builder.Services.AddSwaggerGen(options =>
            {
                // Enable [SwaggerRequestExample] filter for parameters in Swagger UI
                options.ExampleFilters();

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

            // Swagger UI: Examples (showing custom values of API parameters instead of the default ones)
            builder.Services.AddSwaggerExamplesFromAssemblyOf<WeatherForecastDtoExample>();

            // Version of the application
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(ApiCommonValues.Version.Default);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            return builder;
        }

        // TODO: Introduce generic registration of Scoped or Transient services, not only Singletons
        private static WebApplicationBuilder RegisterInternalServices(this WebApplicationBuilder builder)
        {
            // Resolvers
            builder.Services.AddScoped<IServiceHandler, ServiceHandler>();
            builder.Services.AddScoped<IServiceResolver, ServiceResolver>();

            // Repositories
            builder.Services.AddScoped<WeatherForecastContext>();

            // Handlers
            builder.Services.AddScoped<AddForecastCommandHandler>();
            builder.Services.AddScoped<GetWeeklyForecastCommandHandler>();

            // Commands
            builder.Services.AddScoped<AddForecastCommand>();
            builder.Services.AddScoped<GetWeeklyForecastCommand>();

            return builder;
        }

        private static WebApplication ConfigureHttpPipeline(this WebApplicationBuilder builder)
        {
            WebApplication app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
