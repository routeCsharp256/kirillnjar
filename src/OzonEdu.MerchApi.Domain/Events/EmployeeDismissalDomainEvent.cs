using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;

namespace OzonEdu.MerchApi.Domain.Events
{
    public class EmployeeDismissalDomainEvent : INotification
    {
        public EmployeeDismissalDomainEvent(Employee employee, int merchTypeId)
        {
            Emlpoyee = employee;
        }
        public Employee Emlpoyee { get; private set; }
    }
}