using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using OzonEdu.MerchApi.Infrastructure.Extensions;

namespace OzonEdu.MerchApi.Infrastructure.StartupFilters
{
    public class LoggingStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseWhen(context =>
                    {
                        context.Request.Headers.TryGetValue("content-type", out var value);
                        return value != "application/grpc";
                    }, 
                    builder =>
                    {
                        builder.UseRequestLogging();
                        builder.UseResponseLogging();
                    }
                );
                next(app);
            };
        }
    }
}