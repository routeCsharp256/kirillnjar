using MediatR;
using OzonEdu.MerchApi.Infrastructure.Commands.IssueMerch.Responses;
using OzonEdu.MerchApi.Infrastructure.Models;

namespace OzonEdu.MerchApi.Infrastructure.Commands.IssueMerch
{
    public class IssueMerchCommand : IRequest<IssueMerchCommandResponse>
    {
        public EmployeeDTO Employee { get; set; }
        public int MerchPackTypeId { get; set; }
        public int FromType { get; set; }
    }
}