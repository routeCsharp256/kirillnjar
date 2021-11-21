using System.Collections.Generic;
using System.Text.RegularExpressions;
using OzonEdu.MerchApi.Domain.Exceptions.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate.EmployeeName
{
    public class EmployeeName : ValueObject
    {
        private const string OnlyLettersRegexpPattern = @"^[А-ЯA-Za-zа-я]+$";
        public string Value { get; }

        protected EmployeeName(string value)
        {
            if (!IsValidName(value))
                throw new InvalidNameException($"{this.GetType().Name} \"{value}\" is invalid");
            Value = value;
        }

        private static bool IsValidName(string value)
            => !(value is not null && !Regex.IsMatch(value, OnlyLettersRegexpPattern));
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}