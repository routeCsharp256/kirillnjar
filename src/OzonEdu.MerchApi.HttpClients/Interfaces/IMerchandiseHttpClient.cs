using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.HttpModels.Request;
using OzonEdu.MerchApi.HttpModels.Response;

namespace OzonEdu.HttpClients.Interfaces
{
    public interface IMerchandiseHttpClient
    {
        Task<RequestMerchResponse>
            RequestMerch(RequestMerchPostViewModel postViewModel, CancellationToken token);
        
        Task<IEnumerable<MerchInfoResponse>>
            GetEmployeeMerchById(long employeeId, CancellationToken token);
    }
}