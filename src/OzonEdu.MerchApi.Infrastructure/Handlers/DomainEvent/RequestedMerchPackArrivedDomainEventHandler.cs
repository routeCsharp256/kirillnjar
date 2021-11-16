using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.Events;
using OzonEdu.MerchApi.Enums;
using OzonEdu.MerchApi.Infrastructure.Models;
using OzonEdu.MerchApi.Infrastructure.Services.Interfaces;

namespace OzonEdu.MerchApi.Infrastructure.Handlers.DomainEvent
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
            await _emailService.Send(new EmployeeNotificationEventDTO
            {
                EmployeeEmail = notification.MerchRequest.Employee.Email.Value,
                EmployeeName = $"{notification.MerchRequest.Employee.Name.LastName} " +
                               $"{notification.MerchRequest.Employee.Name.FirstName} " +
                               $"{notification.MerchRequest.Employee.Name.MiddleName} ",
                EventType = EmployeeEventType.MerchArrived,
                Payload = new Payload()
                {
                    MerchType = notification.MerchRequest.MerchPackId
                }
            }, cancellationToken);
        }
    }
}