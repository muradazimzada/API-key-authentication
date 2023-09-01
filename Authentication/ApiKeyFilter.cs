using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace API_key_authentication.Authentication
{
    public class ApiKeyFilter : IAuthorizationFilter
    {
        private readonly IConfiguration configuration;
        public ApiKeyFilter(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

       

        public void  OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApyKeyHeaderName, out var extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("API Key missing");
                return;
            }

            var apiKey = configuration.GetValue<string>(AuthConstants.ApiKeySectionName);

            if (!apiKey.Equals(extractedApiKey))
            {

                context.Result= new UnauthorizedObjectResult("Invalid API Key");
                return;
            }

        }
    }
}
