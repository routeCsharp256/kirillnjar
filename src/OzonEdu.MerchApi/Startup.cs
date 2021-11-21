using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Contracts;
using OzonEdu.MerchApi.Domain.Services;
using OzonEdu.MerchApi.GrpcServices;
using OzonEdu.MerchApi.Infrastructure.Handlers.MerchRequestAggregate;
using OzonEdu.MerchApi.Infrastructure.PipelineBehaviors.UnitOfWorkBehavior;
using OzonEdu.MerchApi.Infrastructure.PipelineBehaviors.ValidationBehavior;
using OzonEdu.MerchApi.Infrastructure.Repositories.Implementation.Mock;
using OzonEdu.MerchApi.Infrastructure.Services.Implementation;

namespace OzonEdu.MerchApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            AddDatabaseComponents(services);
            AddMockServices(services);
            AddMockRepositories(services);
            AddMediator(services);
        }
        

        private static void AddMediator(IServiceCollection services)
        {
            services.AddMediatR(typeof(IssueMerchCommandHandler));
            services.AddValidatorsFromAssembly(typeof(IssueMerchCommandHandler).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
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