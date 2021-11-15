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
using OzonEdu.MerchApi.Infrastructure.Queries.MerchRequestAggregate;
using OzonEdu.MerchApi.Infrastructure.Queries.MerchRequestAggregate.Responses;
using OzonEdu.MerchApi.Infrastructure.Services.Interfaces;

namespace OzonEdu.MerchApi.Infrastructure.Handlers.MerchRequestAggregate
{
    public class GetAllMerchPackByEmployeeQueryHandler : IRequestHandler<GetAllMerchPackByEmployeeQuery, GetAllMerchPackByEmployeeQueryResponse>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;

        public GetAllMerchPackByEmployeeQueryHandler(IMerchRequestRepository merchRequestRepository)
        {
            _merchRequestRepository = merchRequestRepository;
        }

        public async Task<GetAllMerchPackByEmployeeQueryResponse> Handle(GetAllMerchPackByEmployeeQuery request, CancellationToken cancellationToken)
        {
            var givenMerch =
                await _merchRequestRepository.GetDoneByEmployeeEmailAsync(new Email(request.Email), cancellationToken);
            return new GetAllMerchPackByEmployeeQueryResponse
            {
                Items = givenMerch.Select(_ =>
                    new GivenMerchPackDTO
                    {
                        DateGiven = _.MerchRequestDateTime.Value,
                        MerchPackTypeId = _.MerchPackId
                    }).ToList()
            };
        }
    }
}