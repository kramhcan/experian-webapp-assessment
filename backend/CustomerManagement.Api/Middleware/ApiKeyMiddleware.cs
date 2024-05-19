using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.Api.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _apiKey;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _apiKey = configuration?["ApiKey"];
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("x-api-key"))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key is missing.");
                return;
            }

            var apiKey = context.Request.Headers["x-api-key"];
            if (apiKey != _apiKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Api Key.");
                return;
            }

            await _next(context);
        }
        
    }
}