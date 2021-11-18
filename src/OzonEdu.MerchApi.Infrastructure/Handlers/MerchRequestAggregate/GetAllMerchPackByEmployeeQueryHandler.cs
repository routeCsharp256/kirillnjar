using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Infrastructure.Models;
using OzonEdu.MerchApi.Infrastructure.Queries.MerchRequestAggregate;
using OzonEdu.MerchApi.Infrastructure.Queries.MerchRequestAggregate.Responses;

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
                await _merchRequestRepository.GetByEmployeeEmailAndStatusAsync(new Email(request.Email), MerchRequestStatus.Done, cancellationToken);
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