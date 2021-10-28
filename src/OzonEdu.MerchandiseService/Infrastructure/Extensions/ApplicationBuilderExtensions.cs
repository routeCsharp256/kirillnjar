using System;
using Microsoft.AspNetCore.Builder;
using OzonEdu.MerchandiseService.Infrastructure.Middlewares;

namespace OzonEdu.MerchandiseService.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseLive(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<LiveMiddleware>();
        }
        
        public static IApplicationBuilder UseReady(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<ReadyMiddleware>();
        }
        
        public static IApplicationBuilder UseVersion(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<VersionMiddleware>();
        }
        
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
        
        
        public static IApplicationBuilder UseResponseLogging(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<ResponseLoggingMiddleware>();
        }
    }
}