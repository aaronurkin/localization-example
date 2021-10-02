using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalizationInvestigation.Application.Services
{
    public class MicrosoftLocalizerTranslator<TService> : ITranslator<TService>
    {
        private readonly IEnumerable<IStringLocalizer> localizers;

        public MicrosoftLocalizerTranslator(IStringLocalizerFactory localizerFactory)
        {
            if (localizerFactory == null)
            {
                throw new ArgumentNullException(nameof(localizerFactory));
            }

            this.localizers = new[]
            {
                typeof(TService),
                typeof(CommonTranslations)
            }
            .Select(localizer =>
            {
                var location = localizer.AssemblyQualifiedName?.Split(',')[1];

                if (string.IsNullOrEmpty(location))
                {
                    return null;
                }

                return localizerFactory.Create(localizer.Name, location);
            })
            .Where(localizer => localizer != null);
        }

        public string Translate(string key)
        {
            return this.Translate(key, new object[0]);
        }

        public string Translate(string key, params object[] arguments)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            foreach (var localizer in this.localizers)
            {
                var translation = arguments.Length < 1
                    ? localizer[key]
                    : localizer[key, arguments];

                if (!key.Equals(translation))
                {
                    return translation;
                }
            }

            return key;
        }
    }
}
