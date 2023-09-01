namespace API_key_authentication.Authentication
{
    public class ApiKeyMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context)
        {

            if(!context.Request.Headers.TryGetValue(AuthConstants.ApyKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key missing");
                return;
            }

            var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
            
            if(!apiKey.Equals(extractedApiKey))
            {

                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key mismatched");
                return;
            }

            await _next(context);
        }
    }
}
