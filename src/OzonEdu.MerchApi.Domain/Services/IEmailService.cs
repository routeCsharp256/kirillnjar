using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.Events;

namespace OzonEdu.MerchApi.Domain.Services
{
    public interface IEmailService
    {
        public Task Send(RequestedMerchPackArrivedDomainEvent notification, CancellationToken cancellationToken);
        public Task Send(MerchPackReservationSuccessDomainEvent notification, CancellationToken cancellationToken);
    }
}