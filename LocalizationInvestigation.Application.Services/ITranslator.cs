using Microsoft.Extensions.Localization;
using System;
using System.Linq;

namespace LocalizationInvestigation.Application.Services
{
    public interface ITranslator<TService>
    {
        string Translate(string key);
        string Translate(string key, params object[] args);
    }

    public class MicrosoftLocalizerTranslator<TService> : ITranslator<TService>
    {
        private readonly IStringLocalizer<TService> serviceLocalizer;
        private readonly IStringLocalizer<CommonTranslations> commonLocalizer;

        public MicrosoftLocalizerTranslator(
            IStringLocalizer<TService> serviceLocalizer,
            IStringLocalizer<CommonTranslations> commonLocalizer
        )
        {
            this.commonLocalizer = commonLocalizer ?? throw new ArgumentNullException(nameof(commonLocalizer));
            this.serviceLocalizer = serviceLocalizer ?? throw new ArgumentNullException(nameof(serviceLocalizer));
        }

        public string Translate(string key)
        {
            return this.Translate(key, new object[0]);
        }

        public string Translate(string key, params object[] arguments)
        {
            var translation = !arguments.Any()
                ? this.serviceLocalizer[key]
                : this.serviceLocalizer[key, arguments];

            if (key.Equals(translation))
            {
                translation = !arguments.Any()
                    ? this.commonLocalizer[key]
                    : this.commonLocalizer[key, arguments];
            }

            return translation;
        }
    }
}
