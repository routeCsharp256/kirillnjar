using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Events;
using OzonEdu.MerchApi.Domain.Services;

namespace OzonEdu.MerchApi.Services.Handlers.DomainEvent
{
    public class MerchPackReservationSuccessDomainEventHandler: INotificationHandler<MerchPackReservationSuccessDomainEvent>
    {
        private readonly IEmailService _emailService;
        
        public MerchPackReservationSuccessDomainEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(MerchPackReservationSuccessDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.MerchRequest.MerchRequestFrom.Id == MerchRequestFromType.Automatically.Id)
            {
                await _emailService.Send(notification, cancellationToken);
            }
        }
    }
}