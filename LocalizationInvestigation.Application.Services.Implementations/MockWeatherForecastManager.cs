using LocalizationInvestigation.Application.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LocalizationInvestigation.Application.Services
{
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
            "FORECAST_SUMMARY_SCORCHING",
            "COMMON"
        };

        private readonly ITranslator<MockWeatherForecastManager> translator;

        public MockWeatherForecastManager(ITranslator<MockWeatherForecastManager> translator)
        {
            this.translator = translator ?? throw new ArgumentNullException(nameof(translator));
        }

        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = this.translator.Translate(Summaries[rng.Next(Summaries.Length)], CultureInfo.CurrentCulture.Name)
            })
            .ToArray();
        }
    }
}
