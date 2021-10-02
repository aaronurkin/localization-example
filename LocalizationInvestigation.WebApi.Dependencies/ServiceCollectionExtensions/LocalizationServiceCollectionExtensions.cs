using LocalizationInvestigation.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LocalizationServiceCollectionExtensions
    {
        public static IServiceCollection AddLocalization(this IServiceCollection services, IConfiguration configuration)
        {
            const string LOCALIZATION_TRANSLATIONS_PATH_SECTION = "Localization:TranslationsPath";

            var translationsPath = configuration[LOCALIZATION_TRANSLATIONS_PATH_SECTION];

            if (string.IsNullOrEmpty(translationsPath))
            {
                // TODO: Replace with correct exception type
                throw new System.Exception($"{LOCALIZATION_TRANSLATIONS_PATH_SECTION} section is missing");
            }

            services
                .AddSingleton(typeof(ITranslator<>), typeof(MicrosoftLocalizerTranslator<>));

            services
                .AddLocalization(options => { options.ResourcesPath = translationsPath; });

            return services;
        }
    }
}
