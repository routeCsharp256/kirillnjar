using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Events;
using OzonEdu.MerchApi.Infrastructure.Commands.IssueMerch;
using OzonEdu.MerchApi.Infrastructure.Models;

namespace OzonEdu.MerchApi.Infrastructure.Handlers.DomainEvent
{
    public class SupplyArrivedWithStockItemsDomainEventHandler : INotificationHandler<SupplyArrivedWithStockItemsDomainEvent>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IMediator _mediator;
        
        public SupplyArrivedWithStockItemsDomainEventHandler(IMerchRequestRepository merchRequestRepository
            , IMerchPackRepository merchPackRepository
            , IMediator mediator)
        {
            _merchRequestRepository = merchRequestRepository;
            _merchPackRepository = merchPackRepository;
            _mediator = mediator;
        }
        
        public async Task Handle(SupplyArrivedWithStockItemsDomainEvent notification, CancellationToken cancellationToken)
        {
            var arrivedMerchPacks = await _merchPackRepository.Get(notification.Items.Keys.ToList(), cancellationToken);
            foreach (var arrivedMerchPack in arrivedMerchPacks.OrderByDescending(mp => mp.Id))
            {
                var requests = await
                    _merchRequestRepository.Get(arrivedMerchPack.Type.Id,
                        MerchRequestStatus.AwaitingDelivery, cancellationToken);
                foreach (var request in requests.OrderBy(r => r.MerchRequestDateTime.Value))
                {
                    if (request.MerchRequestFrom.Id == MerchRequestFromType.Manually.Id)
                    {
                        await _mediator.Publish(new RequestedMerchPackArrivedDomainEvent(request)
                            , cancellationToken);
                    }
                    else
                    {
                        await _mediator.Send(new IssueMerchCommand
                            {
                                Employee = new EmployeeDTO
                                {
                                    Email = request.Employee.Email.Value,
                                    FirstName = request.Employee.Name.FirstName.Value,
                                    LastName = request.Employee.Name.LastName.Value,
                                    MiddleName = request.Employee.Name.MiddleName.Value
                                },
                                FromType = request.MerchRequestFrom.Id,
                                MerchPackTypeId = request.MerchPackId
                            }
                            , cancellationToken);
                    }
                }
            }
        }
    }
}