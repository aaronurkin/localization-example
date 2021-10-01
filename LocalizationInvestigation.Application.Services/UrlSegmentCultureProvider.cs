using LocalizationInvestigation.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LocalizationInvestigation.Application.Services
{
    public class UrlSegmentCultureProvider : RouteDataRequestCultureProvider
    {
        private readonly string cultureNamePattern;

        public UrlSegmentCultureProvider(UrlSegmentCultureProviderOptions options)
        {
            this.Options = options;
            this.cultureNamePattern = options.CultureNamePattern;
        }

        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var result = await base.DetermineProviderCultureResult(httpContext);

            if (result != null)
            {
                return result;
            }

            if (Regex.IsMatch(httpContext.Request.Path, this.cultureNamePattern))
            {
                var culture = $"{httpContext.Request.Path}".Split('/')[1];

                if (!string.IsNullOrEmpty(culture))
                {
                    return new ProviderCultureResult(culture, culture);
                }
            }

            return null;
        }
    }
}
