using OzonEdu.MerchApi.Domain.Exceptions.EmployeeAggregate;

namespace OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate.EmployeeName
{
    public sealed class LastName : EmployeeName
    {
        private LastName(string value) : base(value) { }

        public static LastName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidNameException($"{nameof(LastName)} {value} can't be null or empty");
            return new LastName(value);
        }
    }
}