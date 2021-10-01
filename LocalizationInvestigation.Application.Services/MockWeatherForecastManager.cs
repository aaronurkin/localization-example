using LocalizationInvestigation.Application.Models;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LocalizationInvestigation.Application.Services
{
    public interface IWeatherForecastManager
    {
        IEnumerable<WeatherForecast> GetWeatherForecast();
    }

    public class MockWeatherForecastManager : IWeatherForecastManager
    {
        private static readonly string[] Summaries = new[]
        {
            "FORECAST_SUMMARY_FREEZING",
            "FORECAST_SUMMARY_BRACING",
            "FORECAST_SUMMARY_CHILLY",
            "FORECAST_SUMMARY_COOL",
            "FORECAST_SUMMARY_MILD",
            "FORECAST_SUMMARY_WARM",
            "FORECAST_SUMMARY_BALMY",
            "FORECAST_SUMMARY_HOT",
            "FORECAST_SUMMARY_SWELTERING",
            "FORECAST_SUMMARY_SCORCHING"
        };

        private readonly IStringLocalizer<MockWeatherForecastManager> translate;

        public MockWeatherForecastManager(IStringLocalizer<MockWeatherForecastManager> translate)
        {
            this.translate = translate ?? throw new ArgumentNullException(nameof(translate));
        }
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = this.translate[Summaries[rng.Next(Summaries.Length)], CultureInfo.CurrentCulture.Name]
            })
            .ToArray();
        }
    }
}
