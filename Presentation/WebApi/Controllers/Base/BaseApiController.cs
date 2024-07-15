using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherForecastApp.Domain.Constants;

namespace WeatherForecastApp.Persistence.Controllers.Base
{
    /// <summary>
    /// Base controller for all API controllers in this application.
    /// </summary>
    [ApiController]
    // Default response status codes for all API controllers
    [ProducesResponseType(StatusCodes.Status200OK,                  Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest,          Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
    // Default data contracts (JSON)
    [Consumes(CommonValues.Request.ContentType)]
    [Produces(CommonValues.Request.ContentType)]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
