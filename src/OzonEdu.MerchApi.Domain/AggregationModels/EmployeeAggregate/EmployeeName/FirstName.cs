using OzonEdu.MerchApi.Domain.Exceptions.EmployeeAggregate;

namespace OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate.EmployeeName
{
    public sealed class FirstName : EmployeeName
    {
        private FirstName(string value) : base(value) { }

        public static FirstName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidNameException($"{nameof(FirstName)} {value} can't be null or empty");
            return new FirstName(value);
        }
    }
}