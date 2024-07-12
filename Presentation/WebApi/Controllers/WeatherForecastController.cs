using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    /// <summary>
    /// Main functionalities of the Weather Forecast App.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public sealed class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
        /// </summary>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        public IEnumerable<object> Get()
        {
            throw new NotImplementedException();
        }
    }
}
