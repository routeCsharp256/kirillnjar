using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using OzonEdu.MerchApi.Enums;
using OzonEdu.MerchApi.Grpc;
using OzonEdu.MerchApi.Infrastructure.Commands.IssueMerch;
using OzonEdu.MerchApi.Infrastructure.Models;
using OzonEdu.MerchApi.Infrastructure.Queries.MerchRequestAggregate;
using StatusType = OzonEdu.MerchApi.Grpc.StatusType;

namespace OzonEdu.MerchApi.GrpcServices
{
    public class MerchandiseGrpcServices : MerchandiseServiceGrpc.MerchandiseServiceGrpcBase
    {
        private readonly IMediator _mediator;

        public MerchandiseGrpcServices(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<RequestMerchandiseResponse> RequestMerchandise(
            RequestMerchandiseRequest request, 
            ServerCallContext context)
        {
            var result = await _mediator.Send(new IssueMerchCommand
            {
                Employee = new EmployeeDTO
                {
                    Email = request.EmployeeEmail,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    MiddleName = request.MiddleName
                },
                FromType = (int) RequestFromType.Manually,
                MerchPackTypeId = request.MerchPackId
            }, context.CancellationToken);
            return new RequestMerchandiseResponse
            {
                IsSuccess = result.IsSuccess,
                StatusType = (StatusType)(int)result.StatusType
            };
        }

        public override async Task<GetEmployeeMerchByIdResponse> GetEmployeeMerchById(
            GetEmployeeMerchByIdRequest request, 
            ServerCallContext context)
        {
            var result = await _mediator.Send(new GetAllMerchPackByEmployeeQuery
            {
                Email = request.EmployeeEmail
            }, context.CancellationToken);

            return new GetEmployeeMerchByIdResponse
            {
                MerchGiven = 
                    {
                        result.Items.Select(mp => new GetEmployeeMerchByIdResponseUnit
                        {
                            TypeId = mp.MerchPackTypeId,
                            DateGiving = mp.DateGiven.ToTimestamp()
                        })
                        
                    }
            };
        }
    }
}