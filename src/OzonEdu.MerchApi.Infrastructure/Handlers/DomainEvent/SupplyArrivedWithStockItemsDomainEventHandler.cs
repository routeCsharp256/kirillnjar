using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Contracts;
using OzonEdu.MerchApi.Domain.Events;
using OzonEdu.MerchApi.Infrastructure.Models;
using OzonEdu.MerchApi.Infrastructure.Services.Interfaces;

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
            , IEmailService emailService
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
            var arrivedMerchPacks = await _merchPackRepository.GetBySkusAsync(notification.Items.Keys.ToList(), cancellationToken);
            foreach (var arrivedMerchPack in arrivedMerchPacks.OrderByDescending(_ => _.Id))
            {
                var requests = await
                    _merchRequestRepository.GetAwaitingDeliveryByMerchPackAsync(arrivedMerchPack.Type.Id,
                        cancellationToken);
                foreach (var request in requests.OrderBy(_ => _.MerchRequestDateTime.Value))
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
                            .TryReserve(arrivedMerchPack.Items.ToDictionary(
                                _ => new MerchItemDTO
                                {
                                    Sku = _.Key.Sku.Value
                                }
                                , _ => _.Value.Value
                            ), cancellationToken);

                        if (isReservedSuccess)
                        {
                            request.SetAsDone(new MerchRequestDateTime(DateTime.UtcNow));
                            await _merchRequestRepository.UpdateAsync(request, cancellationToken);
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