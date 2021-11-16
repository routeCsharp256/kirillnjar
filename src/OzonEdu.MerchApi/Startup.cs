using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Contracts;
using OzonEdu.MerchApi.GrpcServices;
using OzonEdu.MerchApi.Infrastructure.Repositories.Implementation.Mock;
using OzonEdu.MerchApi.Infrastructure.Services.Interfaces;
using OzonEdu.MerchApi.Infrastructure.Services.Interfaces.Implementation;

namespace OzonEdu.MerchApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            AddMediator(services);
            AddDatabaseComponents(services);
            AddMockServices(services);
            AddMockRepositories(services);
        }
        

        private static void AddMediator(IServiceCollection services)
        {    
            var assembly = AppDomain.CurrentDomain.Load("OzonEdu.MerchApi.Infrastructure");
            services.AddMediatR(assembly);
        }
        
        private void AddDatabaseComponents(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWorkMock>();
        }
        private static void AddMockRepositories(IServiceCollection services)
        {
            services.AddSingleton<IMerchPackRepository, MerchPackMockRepository>();
            services.AddSingleton<IMerchRequestRepository, MerchRequestMockRepository>();
        }
        private static void AddMockServices(IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailMockService>();
            services.AddScoped<IStockApiService, StockApiMockService>();
        }
        
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