using System.Collections.Generic;
using System.Text.RegularExpressions;
using OzonEdu.MerchApi.Domain.Exceptions.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate
{
    public class Email : ValueObject
    {
        public Email(string value)
        {
            if (IsValidEmail(value))
                throw new InvalidEmailException($"Employee email \"{value}\" is not valid");
            Value = value;
        }
        private static bool IsValidEmail(string emailString)
            => !string.IsNullOrWhiteSpace(emailString)
                && Regex.IsMatch(emailString, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}