using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WeatherForecastApp.WebApi.Constants;
using WeatherForecastApp.WebApi.Controllers.Base;

namespace WeatherForecastApp.WebApi.Controllers.v1
{
    /// <summary>
    /// Main functionalities of the Weather Forecast App.
    /// </summary>
    [ApiVersion(ApiCommonValues.Version.Default)]
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
