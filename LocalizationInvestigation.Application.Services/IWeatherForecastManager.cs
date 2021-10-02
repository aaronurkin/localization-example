using LocalizationInvestigation.Application.Models;
using System.Collections.Generic;

namespace LocalizationInvestigation.Application.Services
{
    public interface IWeatherForecastManager
    {
        IEnumerable<WeatherForecast> GetWeatherForecast();
    }
}
