using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Contracts;
using OzonEdu.MerchApi.Domain.Events;
using OzonEdu.MerchApi.Domain.Models;
using OzonEdu.MerchApi.Enums;
using OzonEdu.MerchApi.Infrastructure.Commands.IssueMerch;
using OzonEdu.MerchApi.Infrastructure.Commands.MerchIssue;
using OzonEdu.MerchApi.Infrastructure.Models;
using OzonEdu.MerchApi.Infrastructure.Services.Interfaces;

namespace OzonEdu.MerchApi.Infrastructure.Handlers.MerchRequestAggregate
{
    public class IssueMerchCommandHandler : IRequestHandler<IssueMerchCommand, IssueMerchCommandResponse>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IUnitOfWork _merchRequestUnitOfWork;
        private readonly IStockApiService _stockApiService;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IMediator _mediator;

        public IssueMerchCommandHandler(IMerchRequestRepository merchRequestRepository
            , IUnitOfWork merchRequestUnitOfWork
            , IMerchPackRepository merchPackRepository 
            , IStockApiService stockApiRepository
            , IMediator mediator)
        {
            _merchPackRepository = merchPackRepository;
            _merchRequestUnitOfWork = merchRequestUnitOfWork;
            _merchRequestRepository = merchRequestRepository;
            _stockApiService = stockApiRepository;
            _mediator = mediator;
        }

        public async Task<IssueMerchCommandResponse> Handle(IssueMerchCommand request, CancellationToken cancellationToken)
        {
            await _merchRequestUnitOfWork.StartTransaction(cancellationToken);
            var previousRequests =
                await _merchRequestRepository.GetByEmployeeEmailAndMerchPackTypeAsync(
                    new Email(request.Employee.Email)
                    , request.MerchPackTypeId
                    , cancellationToken);
            
            if (previousRequests.Any(_ => _.IsIssuedLessYear(DateTime.UtcNow)))
            {
                return new IssueMerchCommandResponse
                {
                    IsSuccess = false,
                    StatusType = StatusType.AlreadyGiven,
                };
            }

            var merchRequest = new MerchRequest(
                new Employee(
                    new Email(request.Employee.Email)
                    , new EmployeeFullName(request.Employee.FirstName, request.Employee.LastName, request.Employee.MiddleName))
                , request.MerchPackTypeId
                , new MerchRequestDateTime(DateTime.UtcNow)
                , new MerchRequestFrom(
                    Enumeration
                        .GetAll<MerchRequestFromType>()
                        .FirstOrDefault(it => it.Id.Equals(request.FromType))));

            var merchPack = await _merchPackRepository.GetByTypeIdAsync(request.MerchPackTypeId, cancellationToken);
            var isReservedSuccess = await _stockApiService
                .TryReserve(merchPack.Items.ToDictionary(
                    _ => new MerchItemDTO
                    {
                        Sku = _.Key.Sku.Value
                    }
                    , _ => _.Value.Value
                    ), cancellationToken);

            if (isReservedSuccess)
            {
                merchRequest.SetAsDone(new MerchRequestDateTime(DateTime.UtcNow));
                await _mediator.Publish(new MerchPackReservationSuccessDomainEvent(merchRequest)
                    , cancellationToken);
            }
            else
            {
                merchRequest.SetAsAwaitingDelivery(new MerchRequestDateTime(DateTime.UtcNow));
                await _mediator.Publish(new MerchPackReservationFailureDomainEvent(merchRequest)
                    , cancellationToken);
            }
            await _merchRequestUnitOfWork.SaveChangesAsync(cancellationToken);
            
            return new IssueMerchCommandResponse
            {
                IsSuccess = isReservedSuccess,
                StatusType = isReservedSuccess ? StatusType.Reserved : StatusType.OutOfStock
            };
        }
    }
}