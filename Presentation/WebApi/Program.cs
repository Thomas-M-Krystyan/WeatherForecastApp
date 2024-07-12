using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using WeatherForecastApp.WebApi.Constants;

namespace WebApi
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            WebApplication.CreateBuilder(args)
                .RegisterDotNetServices()
                .ConfigureHttpPipeline()
                .Run();
        }

        private static WebApplicationBuilder RegisterDotNetServices(this WebApplicationBuilder builder)
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
                options.SwaggerDoc(CommonValues.Swagger.OpenApiVersion, new OpenApiInfo
                {
                    Version = CommonValues.Swagger.OpenApiVersion,
                    Title = CommonValues.Swagger.Title,
                    Description = CommonValues.Swagger.Description,
                });

            });

            return builder;
        }

        private static WebApplication ConfigureHttpPipeline(this WebApplicationBuilder builder)
        {
            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options
                    => options.SwaggerEndpoint("/swagger/v1/swagger.json", CommonValues.Swagger.Endpoint));
            }

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
