using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.HttpModels.Request;
using OzonEdu.MerchandiseService.HttpModels.Response;

namespace OzonEdu.MerchandiseService.Controllers.V1
{
    [ApiController]
    [Route("v1/api/merchandise")]
    [Produces("application/json")]
    public class MerchControllerController : ControllerBase
    {
        public MerchControllerController()
        {
        }

        [HttpPost]
        public Task<ActionResult<RequestMerchResponse>> RequestMerch(RequestMerchPostViewModel postViewModel ,CancellationToken token)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        public Task<ActionResult<IEnumerable<MerchInfoResponse>>> GetEmployeeMerchById([FromQuery]long employeeId, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}