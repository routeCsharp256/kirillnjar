using MediatR;
using OzonEdu.MerchApi.Infrastructure.Models;

namespace OzonEdu.MerchApi.Infrastructure.Commands.EmployeeDismissalCommand
{
    public class EmployeeDismissalCommand : IRequest<Unit>
    {
        public EmployeeDTO Employee { get; set; }
    }
}