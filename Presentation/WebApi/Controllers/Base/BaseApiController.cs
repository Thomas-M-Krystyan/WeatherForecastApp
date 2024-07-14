using Microsoft.AspNetCore.Mvc;

namespace WeatherForecastApp.WebApi.Controllers.Base
{
    /// <summary>
    /// Base controller for all API controllers in this application.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
