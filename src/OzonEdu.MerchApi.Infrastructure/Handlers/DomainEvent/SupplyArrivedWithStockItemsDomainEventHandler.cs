using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Contracts;
using OzonEdu.MerchApi.Domain.Events;
using OzonEdu.MerchApi.Domain.Services;
using OzonEdu.MerchApi.Infrastructure.Models;

namespace OzonEdu.MerchApi.Infrastructure.Handlers.DomainEvent
{
    public class SupplyArrivedWithStockItemsDomainEventHandler : INotificationHandler<SupplyArrivedWithStockItemsDomainEvent>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IUnitOfWork _merchRequestUnitOfWork;
        private readonly IStockApiService _stockApiService;
        private readonly IMediator _mediator;
        
        public SupplyArrivedWithStockItemsDomainEventHandler(IMerchRequestRepository merchRequestRepository
            , IMerchPackRepository merchPackRepository
            , IUnitOfWork merchRequestUnitOfWork
            , IStockApiService stockApiService
            , IMediator mediator)
        {
            _merchRequestRepository = merchRequestRepository;
            _merchPackRepository = merchPackRepository;
            _merchRequestUnitOfWork = merchRequestUnitOfWork;
            _stockApiService = stockApiService;
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
                        await _merchRequestUnitOfWork.StartTransaction(cancellationToken);
                        var isReservedSuccess = await _stockApiService
                            .TryReserve(arrivedMerchPack, cancellationToken);

                        if (isReservedSuccess)
                        {
                            request.SetAsDone(MerchRequestDateTime.Create(DateTime.UtcNow));
                            await _merchRequestRepository.Update(request, cancellationToken);
                            await _mediator.Publish(new MerchPackReservationSuccessDomainEvent(request)
                                , cancellationToken);
                        }
                        
                        await _merchRequestUnitOfWork.SaveChangesAsync(cancellationToken);
                    }
                }
            }
        }
    }
}