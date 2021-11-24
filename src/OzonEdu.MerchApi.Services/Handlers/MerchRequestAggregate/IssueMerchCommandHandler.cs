using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Exceptions.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Models;
using OzonEdu.MerchApi.Domain.Services;
using OzonEdu.MerchApi.Enums;
using OzonEdu.MerchApi.Services.Commands.IssueMerch;
using OzonEdu.MerchApi.Services.Commands.IssueMerch.Responses;

namespace OzonEdu.MerchApi.Services.Handlers.MerchRequestAggregate
{
    public class IssueMerchCommandHandler : IRequestHandler<IssueMerchCommand, IssueMerchCommandResponse>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IStockApiService _stockApiService;
        private readonly IMerchPackRepository _merchPackRepository;

        public IssueMerchCommandHandler(IMerchRequestRepository merchRequestRepository
            , IMerchPackRepository merchPackRepository 
            , IStockApiService stockApiRepository)
        {
            _merchRequestRepository = merchRequestRepository;
            _merchPackRepository = merchPackRepository;
            _stockApiService = stockApiRepository;
        }

        public async Task<IssueMerchCommandResponse> Handle(IssueMerchCommand command, CancellationToken cancellationToken)
        {
            var merchPacks = await _merchPackRepository.Get(command.MerchPackTypeId, cancellationToken);
            if (!merchPacks.Any())
                throw new MerchPackNotFoundException($"Merch pack with id {command.MerchPackTypeId} not found");
            
            if (merchPacks.Count > 1)
                throw new MerchPackMultipleFoundException($"Founded multiple actual merch packs with Id {command.MerchPackTypeId}");

            var merchPack = merchPacks.Single();
            
            var previousRequests = await _merchRequestRepository.Get(Email.Create(command.Employee.Email),
                    command.MerchPackTypeId, cancellationToken);

            if (!CanCreate(previousRequests)) return _alreadyGivenResponse;

            var merchRequest = previousRequests.FirstOrDefault(mr => mr.MerchRequestStatus.Equals(MerchRequestStatus.AwaitingDelivery))
                               ?? await _merchRequestRepository.Create(ToMerchRequest(command), cancellationToken);

            var isReservedSuccess = await _stockApiService.TryReserve(merchPack, cancellationToken);
            IssueMerchCommandResponse response;
            if (isReservedSuccess)
            {
                merchRequest.SetAsDone(MerchRequestDateTime.Create(DateTime.UtcNow));
                response = _reservedResponse;
            }
            else
            {
                merchRequest.SetAsAwaitingDelivery(MerchRequestDateTime.Create(DateTime.UtcNow));
                response = _outOfStockResponse;
            }
            await _merchRequestRepository.Update(merchRequest, cancellationToken);
            return response;
        }

        private static bool CanCreate(IReadOnlyList<MerchRequest> previousRequests) =>
            !previousRequests.Any(mr 
                => mr.IsIssuedLessYear(DateTime.UtcNow)
                   && mr.MerchRequestStatus.Equals(MerchRequestStatus.Done));
        
        private static MerchRequest ToMerchRequest(IssueMerchCommand command)
        {
            return new MerchRequest(
                new Employee(
                    Email.Create(command.Employee.Email)
                    , FullName.Create(command.Employee.FirstName, command.Employee.LastName, command.Employee.MiddleName))
                , command.MerchPackTypeId
                , MerchRequestDateTime.Create(DateTime.UtcNow)
                , new MerchRequestFrom(
                    Enumeration
                        .GetAll<MerchRequestFromType>()
                        .FirstOrDefault(it => it.Id.Equals(command.FromType))));
        }
        
        #region responses
        private readonly IssueMerchCommandResponse _alreadyGivenResponse =  new()
        {
            IsSuccess = false,
            StatusType = StatusType.AlreadyGiven,
        };

        private readonly IssueMerchCommandResponse _outOfStockResponse =  new()
        {
            IsSuccess = false,
            StatusType = StatusType.OutOfStock,
        };
        
        private readonly IssueMerchCommandResponse _reservedResponse =  new()
        {
            IsSuccess = true,
            StatusType = StatusType.Reserved,
        };
        
        #endregion
    }
}