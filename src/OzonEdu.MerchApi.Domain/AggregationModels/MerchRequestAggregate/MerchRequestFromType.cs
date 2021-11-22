using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequestFromType : Enumeration
    {
        
        public static MerchRequestFromType Manually  = new(1, nameof(Manually));
        public static MerchRequestFromType Automatically  = new(2, nameof(Automatically));
        
        public MerchRequestFromType(int id, string name) : base(id, name)
        {
            
        }
    }
}