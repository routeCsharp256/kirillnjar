using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Contracts;
using OzonEdu.MerchApi.Domain.Events;

namespace OzonEdu.MerchApi.Infrastructure.Handlers.DomainEvent
{
    public class EmployeeDismissalDomainEventHandler : INotificationHandler<EmployeeDismissalDomainEvent>
    {
        private readonly IMerchRequestRepository _merchRequest;
        private readonly IUnitOfWork _unitOfWork;
        
        public EmployeeDismissalDomainEventHandler(IMerchRequestRepository merchRequest, IUnitOfWork unitOfWork)
        {
            _merchRequest = merchRequest;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EmployeeDismissalDomainEvent notification, CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);
            var requests = await _merchRequest.GetNotDoneByEmployeeEmailAsync(notification.Emlpoyee.Email, cancellationToken);
            foreach (var request in requests)
            {
                request.SetAsCanceled(new MerchRequestDateTime(DateTime.UtcNow));
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}