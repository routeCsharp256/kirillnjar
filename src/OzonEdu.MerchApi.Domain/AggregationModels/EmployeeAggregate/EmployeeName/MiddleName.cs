namespace OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate.EmployeeName
{
    public sealed class MiddleName : EmployeeName
    {
        private MiddleName(string value) : base(value) { }

        public static MiddleName Create(string value)
        {
            return new MiddleName(value);
        }
    }
}