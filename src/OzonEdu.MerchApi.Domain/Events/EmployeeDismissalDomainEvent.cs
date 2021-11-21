using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.Exceptions;

namespace OzonEdu.MerchApi.Domain.Events
{
    public class EmployeeDismissalDomainEvent : INotification
    {
        public EmployeeDismissalDomainEvent(Employee employee)
        {
            Employee = employee  ?? throw new RequiredEventPropertyIsNullException(nameof(employee),
                $"{nameof(EmployeeDismissalDomainEvent)} can't be created without {nameof(employee)}");;
        }
        public Employee Employee { get; }
    }
}