using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Services.Models;
using OzonEdu.MerchApi.Services.Queries.MerchRequestAggregate;
using OzonEdu.MerchApi.Services.Queries.MerchRequestAggregate.Responses;

namespace OzonEdu.MerchApi.Services.Handlers.MerchRequestAggregate
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
                await _merchRequestRepository.Get(Email.Create(request.Email), MerchRequestStatus.Done, cancellationToken);
            return new GetAllMerchPackByEmployeeQueryResponse
            {
                Items = givenMerch.Select(mr =>
                    new GivenMerchPackDTO
                    {
                        DateGiven = mr.MerchRequestDateTime.Value,
                        MerchPackTypeId = mr.MerchPackId
                    }).ToList()
            };
        }
    }
}