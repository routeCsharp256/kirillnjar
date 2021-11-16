using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchApi.Enums;
using OzonEdu.MerchApi.HttpModels.Request;
using OzonEdu.MerchApi.HttpModels.Response;
using OzonEdu.MerchApi.Infrastructure.Commands.IssueMerch;
using OzonEdu.MerchApi.Infrastructure.Models;
using OzonEdu.MerchApi.Infrastructure.Queries.MerchRequestAggregate;

namespace OzonEdu.MerchApi.Controllers.V1
{
    [ApiController]
    [Route("v1/api/merchandise")]
    [Produces("application/json")]
    public class MerchControllerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MerchControllerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<RequestMerchResponse> RequestMerch(RequestMerchPostViewModel postViewModel ,CancellationToken token)
        {
            var result = await _mediator.Send(new IssueMerchCommand
            {
                Employee = new EmployeeDTO
                {
                    Email = postViewModel.EmployeeEmail,
                    FirstName = postViewModel.FirstName,
                    LastName = postViewModel.LastName,
                    MiddleName = postViewModel.MiddleName
                },
                FromType = (int) RequestFromType.Manually,
                MerchPackTypeId = postViewModel.MerchPackId
            }, token);
            return new RequestMerchResponse
            {
                IsSuccess = result.IsSuccess,
                StatusType = result.StatusType
            };
        }
        
        [HttpGet]
        public async Task<IEnumerable<MerchInfoResponse>> GetEmployeeMerchByEmail([FromQuery]string employeeEmail, CancellationToken token)
        {
            var result = await _mediator.Send(new GetAllMerchPackByEmployeeQuery
            {
                Email = employeeEmail
            }, token);

            return result.Items.Select(_ => new MerchInfoResponse
            {
                TypeId = _.MerchPackTypeId,
                DateGiving = _.DateGiven
            });
        }
    }
}