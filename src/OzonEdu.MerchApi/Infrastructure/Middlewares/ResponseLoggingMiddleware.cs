using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace OzonEdu.MerchApi.Infrastructure.Middlewares
{
    public class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseLoggingMiddleware> _logger;

        public ResponseLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            await LogResponseAsync(context);
        }

        private void LogResponse(HttpContext context)
        {
            try
            {
                var logString = new StringBuilder();
                logString.Append(context.Request.Method)
                    .Append(context.Request.GetEncodedPathAndQuery())
                    .Append('\n');
                foreach (var headerKey in context.Response.Headers.Keys)
                {
                    logString.Append(headerKey)
                        .Append(" - ")
                        .Append(context.Response.Headers[headerKey])
                        .Append('\n');
                }
                _logger.LogInformation($"Response\n {logString}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log response");
            }
        }

        private async Task LogResponseAsync(HttpContext context) =>
            await Task.Run(() => LogResponse(context));
    }
}