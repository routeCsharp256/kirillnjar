using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchApi.Infrastructure.Interceptors;
using OzonEdu.MerchApi.GrpcServices;

namespace OzonEdu.MerchApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) { }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchandiseGrpcServices>();
                endpoints.MapControllers();
            });
        }
    }
}