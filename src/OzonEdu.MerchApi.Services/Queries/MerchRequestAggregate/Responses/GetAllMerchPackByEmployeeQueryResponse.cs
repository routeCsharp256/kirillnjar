using System.Collections.Generic;
using OzonEdu.MerchApi.Services.Models;

namespace OzonEdu.MerchApi.Services.Queries.MerchRequestAggregate.Responses
{
    public class GetAllMerchPackByEmployeeQueryResponse
    {
        public IReadOnlyList<GivenMerchPackDTO> Items { get; set; }
    }
}