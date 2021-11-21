using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.Events;
using OzonEdu.MerchApi.Infrastructure.Commands.EmployeeDismissalCommand;
using OzonEdu.MerchApi.Infrastructure.Models;

namespace OzonEdu.MerchApi.Infrastructure.Handlers.DomainEvent
{
    public class EmployeeDismissalDomainEventHandler : INotificationHandler<EmployeeDismissalDomainEvent>
    {
        private readonly IMediator _mediator;
      
        public EmployeeDismissalDomainEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(EmployeeDismissalDomainEvent notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new EmployeeDismissalCommand
            {
                Employee = new EmployeeDTO
                {
                    Email = notification.Employee.Email.Value,
                    FirstName = notification.Employee.Name.FirstName.Value,
                    LastName = notification.Employee.Name.LastName.Value,
                    MiddleName = notification.Employee.Name.MiddleName.Value
                }
            }, cancellationToken);
        }
    }
}