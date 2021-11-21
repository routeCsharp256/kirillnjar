using MediatR;
using OzonEdu.MerchApi.Services.Commands.IssueMerch.Responses;
using OzonEdu.MerchApi.Services.Models;

namespace OzonEdu.MerchApi.Services.Commands.IssueMerch
{
    public class IssueMerchCommand : IRequest<IssueMerchCommandResponse>
    {
        public EmployeeDTO Employee { get; set; }
        public int MerchPackTypeId { get; set; }
        public int FromType { get; set; }
    }
}