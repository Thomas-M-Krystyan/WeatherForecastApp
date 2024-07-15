using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastApp.Application.Responses;
using WeatherForecastApp.Domain.Extensions;
using WeatherForecastApp.Domain.Resolvers.Interfaces;
using WeatherForecastApp.Persistence.Commands;
using WeatherForecastApp.Persistence.Controllers.Base;
using WeatherForecastApp.WebApi.Handlers;
using WeatherForecastApp.WebApi.Models.DTOs;
using WeatherForecastApp.WebApi.Utilities.Swagger.Examples;

namespace WeatherForecastApp.Persistence.Controllers.v1
{
    /// <summary>
    /// Main functionalities of the Weather Forecast App.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public sealed class WeatherForecastController : BaseApiController
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IServiceResolver _serviceResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
        /// </summary>
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IServiceResolver serviceResolver)
        {
            this._logger = logger;
            this._serviceResolver = serviceResolver;
        }

        /// <summary>
        /// Adds a single weather forecasst to the repository.
        /// </summary>
        /// <param name="dto">The Data Transfer Object (DTO) to be consumed.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [HttpPost]
        [Route("PostForecast")]
        // Swagger UI
        [SwaggerRequestExample(typeof(WeatherForecastDto), typeof(WeatherForecastDtoExample))]
        public async Task<IActionResult> PostForecastAsync(
            [Required, FromBody] WeatherForecastDto dto, CancellationToken cancellationToken)
        {
            try
            {
                ForecastCommandHandler handler = this._serviceResolver.Resolve<ForecastCommandHandler>();
                QueryCommandResult queryResult = await handler.HandleAsync<AddForecastCommand>(dto, cancellationToken);

                return queryResult.IsSuccess
                    ? Ok(queryResult.ToString())
                    : BadRequest(queryResult.ToString());
            }
            catch (Exception exception)
            {
                this._logger.LogDetailed(exception);

                return UnprocessableEntity(exception);
            }
        }
    }
}
