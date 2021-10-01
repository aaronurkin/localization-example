using Microsoft.AspNetCore.Builder;

namespace LocalizationInvestigation.Application.Models
{
    public class UrlSegmentCultureProviderOptions : RequestLocalizationOptions
    {
        public string CultureNamePattern { get; set; }
    }
}
