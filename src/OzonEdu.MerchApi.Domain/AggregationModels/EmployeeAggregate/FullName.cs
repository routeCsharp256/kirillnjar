using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate.EmployeeName;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate
{
    public class FullName : ValueObject
    {
        public FirstName FirstName { get; }
        public LastName LastName { get; }
        public MiddleName MiddleName { get; }

        public FullName(LastName lastName, FirstName firstName, MiddleName middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public static FullName Create(string lastName, string firstName, string middleName)
        {
            var firstNameObject = FirstName.Create(firstName);
            var lastNameObject = LastName.Create(lastName);
            var middleNameObject = MiddleName.Create(middleName);
            return new FullName(lastNameObject, firstNameObject, middleNameObject);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return MiddleName;
        }
    }
}