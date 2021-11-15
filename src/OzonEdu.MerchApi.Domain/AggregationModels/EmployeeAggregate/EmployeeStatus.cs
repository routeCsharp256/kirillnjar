using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate
{
    public class EmployeeStatus : Enumeration
    {
        
        public static EmployeeStatus Work  = new(1, nameof(Work));
        public static EmployeeStatus Dismissed  = new(2, nameof(Dismissed));

        public EmployeeStatus(int id, string name) : base(id, name)
        {
        }
    }
}