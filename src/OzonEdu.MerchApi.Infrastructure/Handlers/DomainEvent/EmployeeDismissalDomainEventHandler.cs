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
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public EmployeeDismissalDomainEventHandler(IMerchRequestRepository merchRequest, IUnitOfWork unitOfWork)
        {
            _merchRequestRepository = merchRequest;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EmployeeDismissalDomainEvent notification, CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);
            var requests = await _merchRequestRepository.GetByEmployeeEmailAndStatus(notification.Emlpoyee.Email,
                MerchRequestStatus.AwaitingDelivery, cancellationToken);
            foreach (var request in requests)
            {
                request.SetAsCanceled(new MerchRequestDateTime(DateTime.UtcNow));
                await _merchRequestRepository.Update(request, cancellationToken);
            }
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}