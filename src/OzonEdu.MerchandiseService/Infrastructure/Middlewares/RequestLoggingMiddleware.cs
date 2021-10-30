using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace OzonEdu.MerchandiseService.Infrastructure.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequestAsync(context);
            await _next(context);
        }

        private void LogRequest(HttpContext context)
        {
            try
            {
                var logString = new StringBuilder();
                logString.Append(context.Request.Method)
                    .Append(context.Request.GetEncodedPathAndQuery())
                    .Append('\n');
                foreach (var headerKey in context.Request.Headers.Keys)
                {
                    logString.Append(headerKey)
                        .Append(" - ")
                        .Append(context.Request.Headers[headerKey])
                        .Append('\n');
                }

                _logger.LogInformation($"Request\n {logString}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log request");
            }
        }
        
        private async Task LogRequestAsync(HttpContext context) =>
            await Task.Run(() => LogRequest(context));
    }
}