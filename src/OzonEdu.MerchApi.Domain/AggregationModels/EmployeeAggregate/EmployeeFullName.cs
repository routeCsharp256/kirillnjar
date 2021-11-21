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
        private const string OnlyLettersRegexpPattern = @"^[А-ЯA-Za-zа-я]+$";

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
               && Regex.IsMatch(firstName, OnlyLettersRegexpPattern)
               && Regex.IsMatch(lastName, OnlyLettersRegexpPattern)
               && !(middleName is not null && !Regex.IsMatch(middleName,OnlyLettersRegexpPattern)));
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return MiddleName;
        }
    }
}