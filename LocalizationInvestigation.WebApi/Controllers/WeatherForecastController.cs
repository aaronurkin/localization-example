using LocalizationInvestigation.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LocalizationInvestigation.WebApi.Controllers
{
    [ApiController]
    [Route("{culture}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastManager weatherForecastManager;

        public WeatherForecastController(IWeatherForecastManager weatherForecastManager)
        {
            this.weatherForecastManager = weatherForecastManager ?? throw new ArgumentNullException(nameof(weatherForecastManager));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = this.weatherForecastManager.GetWeatherForecast();
            return this.Ok(data);
        }
    }
}
