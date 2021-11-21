using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate.EmployeeName;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Events;
using OzonEdu.MerchApi.Domain.Exceptions.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Models;
using OzonEdu.MerchApi.Domain.Services;
using OzonEdu.MerchApi.Enums;
using OzonEdu.MerchApi.Infrastructure.Commands.IssueMerch;
using OzonEdu.MerchApi.Infrastructure.Commands.IssueMerch.Responses;

namespace OzonEdu.MerchApi.Infrastructure.Handlers.MerchRequestAggregate
{
    public class IssueMerchCommandHandler : IRequestHandler<IssueMerchCommand, IssueMerchCommandResponse>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IStockApiService _stockApiService;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IMediator _mediator;

        public IssueMerchCommandHandler(IMerchRequestRepository merchRequestRepository
            , IMerchPackRepository merchPackRepository 
            , IStockApiService stockApiRepository
            , IMediator mediator)
        {
            _merchRequestRepository = merchRequestRepository;
            _merchPackRepository = merchPackRepository;
            _stockApiService = stockApiRepository;
            _mediator = mediator;
        }

        public async Task<IssueMerchCommandResponse> Handle(IssueMerchCommand request, CancellationToken cancellationToken)
        {
            var previousRequests =
                await _merchRequestRepository.Get(
                    Email.Create(request.Employee.Email)
                    , request.MerchPackTypeId
                    , cancellationToken);

            
            var merchPack = await _merchPackRepository.Get(request.MerchPackTypeId, cancellationToken)
                            ?? throw new MerchPackNotFoundException($"Merch pack with id {request.MerchPackTypeId} not found");
            
            if (previousRequests.Any(mr => mr.IsIssuedLessYear(DateTime.UtcNow)
                                           && mr.MerchRequestStatus.Equals(MerchRequestStatus.Done)))
            {
                return new IssueMerchCommandResponse
                {
                    IsSuccess = false,
                    StatusType = StatusType.AlreadyGiven,
                };
            }

            var merchRequest =
                previousRequests.FirstOrDefault(mr => mr.MerchRequestStatus.Equals(MerchRequestStatus.AwaitingDelivery))
                ?? await _merchRequestRepository.Create(
                    new MerchRequest(
                        new Employee(
                            Email.Create(request.Employee.Email)
                            , FullName.Create(request.Employee.FirstName, request.Employee.LastName, request.Employee.MiddleName))
                        , merchPack.Id
                        , MerchRequestDateTime.Create(DateTime.UtcNow)
                        , new MerchRequestFrom(
                            Enumeration
                                .GetAll<MerchRequestFromType>()
                                .FirstOrDefault(it => it.Id.Equals(request.FromType)))), cancellationToken);

            var isReservedSuccess = await _stockApiService
                .TryReserve(merchPack, cancellationToken);

            if (isReservedSuccess)
            {
                merchRequest.SetAsDone(MerchRequestDateTime.Create(DateTime.UtcNow));
                await _mediator.Publish(new MerchPackReservationSuccessDomainEvent(merchRequest)
                    , cancellationToken);
            }
            else
            {
                merchRequest.SetAsAwaitingDelivery(MerchRequestDateTime.Create(DateTime.UtcNow));
                await _mediator.Publish(new MerchPackReservationFailureDomainEvent(merchRequest)
                    , cancellationToken);
            }
            
            await _merchRequestRepository.Update(merchRequest, cancellationToken);
            
            return new IssueMerchCommandResponse
            {
                IsSuccess = isReservedSuccess,
                StatusType = isReservedSuccess ? StatusType.Reserved : StatusType.OutOfStock
            };
        }
    }
}