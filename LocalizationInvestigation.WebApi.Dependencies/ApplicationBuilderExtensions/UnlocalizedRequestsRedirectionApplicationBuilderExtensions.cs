using LocalizationInvestigation.Application.Models;
using LocalizationInvestigation.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UnlocalizedRequestsRedirectionApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseUnlocalizedRequestsRedirection(this IApplicationBuilder application, IConfiguration configuration)
        {
            const string LOCALIZATION_SKIP_PATTERN_SECTION = "Localization:SkipPattern";
            const string LOCALIZATION_DEFAULT_CULTURE_SECTION = "Localization:DefaultCulture";
            const string LOCALIZATION_CULTURE_NAME_PATTERN_SECTION = "Localization:CultureNamePattern";

            var skipPattern = configuration[LOCALIZATION_SKIP_PATTERN_SECTION];

            if (string.IsNullOrEmpty(skipPattern))
            {
                // TODO: Replace with correct exception type
                throw new System.Exception($"{LOCALIZATION_SKIP_PATTERN_SECTION} section is missing");
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

            var redirectCultureLessRequestsOptions = new RewriteOptions()
                .Add(new UnlocalizedRequestsRule(new UnlocalizedRequestsOptions
                {
                    SkipPattern = skipPattern,
                    DefaultCulture = defaultCulture,
                    CultureNamePattern = cultureNamePattern,
                }));

            application
                .UseRewriter(redirectCultureLessRequestsOptions);

            return application;
        }
    }
}
