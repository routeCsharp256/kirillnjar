using System;
using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using OzonEdu.MerchandiseService.Infrastructure.Extensions;

namespace OzonEdu.MerchandiseService.Infrastructure.StartupFilters
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