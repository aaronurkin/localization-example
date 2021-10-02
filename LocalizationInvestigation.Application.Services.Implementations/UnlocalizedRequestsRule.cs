using LocalizationInvestigation.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace LocalizationInvestigation.Application.Services
{
    public class UnlocalizedRequestsRule : IRule
    {
        private readonly UnlocalizedRequestsOptions options;

        public UnlocalizedRequestsRule(UnlocalizedRequestsOptions options)
        {
            this.options = options ?? throw new System.ArgumentNullException(nameof(options));
        }

        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;

            if (Regex.IsMatch(request.Path, this.options.SkipPattern))
            {
                return;
            }

            if (!Regex.IsMatch(request.Path, this.options.CultureNamePattern))
            {
                var response = context.HttpContext.Response;

                response.StatusCode = StatusCodes.Status307TemporaryRedirect;
                response.Headers[HeaderNames.Location] = $"{this.options.DefaultCulture}{request.Path}{request.QueryString}";

                context.Result = RuleResult.EndResponse;
            }
        }
    }
}
