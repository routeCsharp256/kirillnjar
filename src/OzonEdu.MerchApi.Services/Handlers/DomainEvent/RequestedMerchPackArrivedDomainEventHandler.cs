using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.Events;
using OzonEdu.MerchApi.Domain.Services;

namespace OzonEdu.MerchApi.Services.Handlers.DomainEvent
{
    public class RequestedMerchPackArrivedDomainEventHandler: INotificationHandler<RequestedMerchPackArrivedDomainEvent>
    {
        private readonly IEmailService _emailService;
        
        public RequestedMerchPackArrivedDomainEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }


        public async Task Handle(RequestedMerchPackArrivedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _emailService.Send(notification, cancellationToken);
        }
    }
}