﻿using LocalizationInvestigation.Application.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IWeatherForecastManager, MockWeatherForecastManager>();

            return services;
        }
    }
}
