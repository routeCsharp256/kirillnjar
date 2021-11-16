using OzonEdu.MerchApi.Domain.Exceptions;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee : Entity
    {
        public Employee(Email email
            , EmployeeFullName name)
        {
            Email = email ?? throw new RequiredEntityPropertyIsNullException(nameof(email), "Employee email can't be null");
            Name = name ?? throw new RequiredEntityPropertyIsNullException(nameof(name), "Employee name can't be null");
            Status = EmployeeStatus.Work;
        }
        public Employee(Email email
            , EmployeeFullName name
            , EmployeeStatus status)
            : this(email, name)
        {
            Status = status;
        }
        
        public Employee(int id, Email email, EmployeeFullName name, EmployeeStatus status)
        : this(email, name, status)
        {
            Id = id;
        }

        public Email Email { get; }
        public EmployeeFullName Name { get; }
        public EmployeeStatus Status { get; set; }

        public void Dissmiss()
        {
            Status = EmployeeStatus.Dismissed;
        }
    }
}