using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchandiseService.GrpcServices;
using OzonEdu.MerchandiseService.Infrastructure.Interceptors;

namespace OzonEdu.MerchandiseService
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