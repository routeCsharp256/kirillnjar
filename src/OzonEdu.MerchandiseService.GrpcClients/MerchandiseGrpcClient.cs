using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Grpc;

namespace OzonEdu.MerchandiseService.GrpcClients
{
    public class MerchandiseGrpcClient
    {
        private readonly MerchandiseServiceGrpc.MerchandiseServiceGrpcClient _client;

        public MerchandiseGrpcClient(MerchandiseServiceGrpc.MerchandiseServiceGrpcClient client)
        {
            _client = client;
        }
        
        public async Task<RequestMerchandiseResponse> RequestMerch(RequestMerchandiseRequest request)
        {
            return await _client.RequestMerchandiseAsync(request);
        }

        public async Task<GetEmployeeMerchByIdResponse> GetEmployeeMerchById(GetEmployeeMerchByIdRequest request)
        {
            return await _client.GetEmployeeMerchByIdAsync(request);
        }
    }
}