using MediatR;
using OzonEdu.MerchApi.Services.Queries.MerchRequestAggregate.Responses;

namespace OzonEdu.MerchApi.Services.Queries.MerchRequestAggregate
{
    public class GetAllMerchPackByEmployeeQuery: IRequest<GetAllMerchPackByEmployeeQueryResponse>
    {
        public string Email { get; set; }
    }
}