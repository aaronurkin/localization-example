using Microsoft.Extensions.Configuration;

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
                .AddLocalization(options => { options.ResourcesPath = translationsPath; });

            return services;
        }
    }
}
