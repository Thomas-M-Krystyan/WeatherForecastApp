using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
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
                options.IncludeXmlComments($"{AppDomain.CurrentDomain.BaseDirectory}\\{nameof(WeatherForecastApp)}.{nameof(WebApi)}.xml");
                options.SwaggerDoc(CommonValues.Swagger.DocumentVersion, new OpenApiInfo
                {
                    Version = CommonValues.Swagger.DocumentVersion,
                    Title = CommonValues.Swagger.Title,
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
