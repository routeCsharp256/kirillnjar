using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee : Entity
    {
        public Employee(Email email
            , EmployeeFullName name)
        {
            Email = email;
            Name = name;
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
        public EmployeeStatus Status { get; }
    }
}