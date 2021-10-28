using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.HttpModels.Request;
using OzonEdu.MerchandiseService.HttpModels.Response;

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