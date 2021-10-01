using LocalizationInvestigation.Application.Models;
using LocalizationInvestigation.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LocalizationApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseLocalization(this IApplicationBuilder application, IConfiguration configuration)
        {
            const string LOCALIZATION_DEFAULT_CULTURE_SECTION = "Localization:DefaultCulture";
            const string LOCALIZATION_SUPPORTED_CULTURES_SECTION = "Localization:SupportedCultures";
            const string LOCALIZATION_CULTURE_NAME_PATTERN_SECTION = "Localization:CultureNamePattern";

            var configuredSupportedCultures = configuration
                .GetSection(LOCALIZATION_SUPPORTED_CULTURES_SECTION)
                .Get<string[]>();

            if (configuredSupportedCultures == null)
            {
                // TODO: Replace with correct exception type
                throw new System.Exception($"{LOCALIZATION_SUPPORTED_CULTURES_SECTION} section is missing");
            }

            var defaultCulture = configuration[LOCALIZATION_DEFAULT_CULTURE_SECTION];

            if (string.IsNullOrEmpty(defaultCulture))
            {
                // TODO: Replace with correct exception type
                throw new System.Exception($"{LOCALIZATION_DEFAULT_CULTURE_SECTION} section is missing");
            }

            var cultureNamePattern = configuration[LOCALIZATION_CULTURE_NAME_PATTERN_SECTION];

            if (string.IsNullOrEmpty(cultureNamePattern))
            {
                // TODO: Replace with correct exception type
                throw new System.Exception($"{LOCALIZATION_CULTURE_NAME_PATTERN_SECTION} section is missing");
            }

            var supportedCultures = configuredSupportedCultures
                    .Select(culture => new CultureInfo(culture))
                    .ToList();

            var options = new UrlSegmentCultureProviderOptions
            {
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures,
                CultureNamePattern = cultureNamePattern,
                DefaultRequestCulture = new RequestCulture(defaultCulture)
            };

            options.RequestCultureProviders.Insert(0, new UrlSegmentCultureProvider(options));

            application
                .UseRequestLocalization(options);

            return application;
        }
    }
}
