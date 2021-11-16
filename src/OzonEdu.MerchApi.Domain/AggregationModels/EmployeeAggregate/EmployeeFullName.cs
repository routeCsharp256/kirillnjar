using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Exceptions.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate
{
    public class EmployeeFullName : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }

        public EmployeeFullName(string firstName, string lastName, string middleName)
        {
            if (string.IsNullOrWhiteSpace(firstName)
                || string.IsNullOrWhiteSpace(lastName)
                || (MiddleName is not null && string.IsNullOrWhiteSpace(middleName)))
                throw new InvalidNameException($"Name {lastName} {firstName} {middleName} is invalid");
            FirstName = firstName;
            LastName = lastName;
            MiddleName = lastName;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return MiddleName;
        }
    }
}