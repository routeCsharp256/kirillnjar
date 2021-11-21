using System.Collections.Generic;
using System.Text.RegularExpressions;
using OzonEdu.MerchApi.Domain.Exceptions.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate
{
    public class Email : ValueObject
    {
        private Email(string value)
        {
            Value = value;
        }
        
        public static Email Create(string emailString)
        {
            if (!IsValidEmail(emailString))
                throw new InvalidEmailException($"Employee email \"{emailString}\" is not valid");
            return new Email(emailString);
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