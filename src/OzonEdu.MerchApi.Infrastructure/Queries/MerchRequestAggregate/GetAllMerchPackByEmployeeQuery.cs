using MediatR;
using OzonEdu.MerchApi.Infrastructure.Queries.MerchRequestAggregate.Responses;

namespace OzonEdu.MerchApi.Infrastructure.Queries.MerchRequestAggregate
{
    public class GetAllMerchPackByEmployeeQuery: IRequest<GetAllMerchPackByEmployeeQueryResponse>
    {
        public string Email { get; set; }
    }
}