using MediatR;
using OzonEdu.MerchApi.Services.Models;

namespace OzonEdu.MerchApi.Services.Commands.EmployeeDismissalCommand
{
    public class EmployeeDismissalCommand : IRequest<Unit>
    {
        public EmployeeDTO Employee { get; set; }
    }
}