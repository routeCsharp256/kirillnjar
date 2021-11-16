using System.Collections.Generic;
using System.Text.RegularExpressions;
using OzonEdu.MerchApi.Domain.Exceptions.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate
{
    public class EmployeeFullName : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }

        public EmployeeFullName(string lastName, string firstName , string middleName)
        {
            if (!IsValidName(firstName, lastName, middleName))
                throw new InvalidNameException($"Name {lastName} {firstName} {middleName} is invalid");
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }


        private static bool IsValidName(string firstName, string lastName, string middleName)
            => (!string.IsNullOrWhiteSpace(firstName)
                && !string.IsNullOrWhiteSpace(lastName)
               && Regex.IsMatch(firstName, @"^[А-ЯA-Za-zа-я]+$")
               && Regex.IsMatch(lastName, @"^[А-ЯA-Za-zа-я]+$")
               && !(middleName is not null && !Regex.IsMatch(middleName, @"^[А-ЯA-Za-zа-я]+$")));
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return MiddleName;
        }
    }
}