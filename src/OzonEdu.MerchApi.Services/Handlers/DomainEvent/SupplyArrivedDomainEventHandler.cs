using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Events;
using OzonEdu.MerchApi.Services.Commands.IssueMerch;
using OzonEdu.MerchApi.Services.Models;

namespace OzonEdu.MerchApi.Services.Handlers.DomainEvent
{
    public class SupplyArrivedDomainEventHandler : INotificationHandler<SupplyArrivedDomainEvent>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IMediator _mediator;
        
        public SupplyArrivedDomainEventHandler(IMerchRequestRepository merchRequestRepository
            , IMerchPackRepository merchPackRepository
            , IMediator mediator)
        {
            _merchRequestRepository = merchRequestRepository;
            _merchPackRepository = merchPackRepository;
            _mediator = mediator;
        }
        
        public async Task Handle(SupplyArrivedDomainEvent notification,
            CancellationToken cancellationToken)
        {
            var arrivedMerchPacks = await _merchPackRepository.Get(
                notification.Items.Keys.ToList(), cancellationToken);
            
            foreach (var merchPack in arrivedMerchPacks.OrderByDescending(mp => mp.Id))
            {
                await ArrivedMerchPackProcess(merchPack, cancellationToken);
            }
        }

        private async Task ArrivedMerchPackProcess(MerchPack arrivedMerchPack, CancellationToken cancellationToken)
        {
            var requests = await
                _merchRequestRepository.Get(arrivedMerchPack.Type.Id,
                    MerchRequestStatus.AwaitingDelivery, cancellationToken);
            foreach (var request in requests.OrderBy(r => r.MerchRequestDateTime.Value))
            {
                await OldRequestProcess(request, cancellationToken);
            }
            
        }

        private async Task OldRequestProcess(MerchRequest oldRequest, CancellationToken cancellationToken)
        {
            
            if (oldRequest.IsAutomatically())
            {
                await _mediator.Send(new IssueMerchCommand
                    {
                        Employee = new EmployeeDTO
                        {
                            Email = oldRequest.Employee.Email.Value,
                            FirstName = oldRequest.Employee.Name.FirstName.Value,
                            LastName = oldRequest.Employee.Name.LastName.Value,
                            MiddleName = oldRequest.Employee.Name.MiddleName.Value
                        },
                        FromType = oldRequest.MerchRequestFrom.Id,
                        MerchPackTypeId = oldRequest.MerchPackId
                    }
                    , cancellationToken);
            }
            else
            {
                await _mediator.Publish(new RequestedMerchPackArrivedDomainEvent(oldRequest)
                    , cancellationToken);
            }
        }
    }
}