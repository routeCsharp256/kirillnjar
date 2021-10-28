using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OzonEdu.MerchandiseService.Grpc;

namespace OzonEdu.MerchandiseService.GrpcServices
{
    public class MerchandiseGrpcServices : MerchandiseServiceGrpc.MerchandiseServiceGrpcBase
    {
        public override async Task<RequestMerchandiseResponse> RequestMerchandise(
            RequestMerchandiseRequest request, 
            ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, "Unimplemented"));
        }

        public override async Task<GetEmployeeMerchByIdResponse> GetEmployeeMerchById(
            GetEmployeeMerchByIdRequest request, 
            ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, "Unimplemented"));
        }
    }
}