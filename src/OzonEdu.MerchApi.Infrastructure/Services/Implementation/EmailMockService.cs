using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.Events;
using OzonEdu.MerchApi.Domain.Services;

namespace OzonEdu.MerchApi.Infrastructure.Services.Implementation
{
    public class EmailMockService : IEmailService
    {
        public Task Send(RequestedMerchPackArrivedDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Send(MerchPackReservationSuccessDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}