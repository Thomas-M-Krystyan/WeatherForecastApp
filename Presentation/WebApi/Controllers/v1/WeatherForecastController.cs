using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WeatherForecastApp.Persistence.Controllers.Base;

namespace WeatherForecastApp.Persistence.Controllers.v1
{
    /// <summary>
    /// Main functionalities of the Weather Forecast App.
    /// </summary>
    [Route("api/v1/[controller]")]
    public sealed class WeatherForecastController : BaseApiController
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
