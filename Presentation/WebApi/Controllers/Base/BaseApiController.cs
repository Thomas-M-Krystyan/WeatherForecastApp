using Microsoft.AspNetCore.Mvc;

namespace WeatherForecastApp.WebApi.Controllers.Base
{
    /// <summary>
    /// Base controller for all API controllers in this application.
    /// </summary>
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
