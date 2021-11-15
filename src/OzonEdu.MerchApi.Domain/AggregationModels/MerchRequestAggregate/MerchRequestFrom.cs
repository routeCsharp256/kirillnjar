using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequestFrom : Entity
    {
        public MerchRequestFromType Type { get; }

        public MerchRequestFrom(MerchRequestFromType type)
        {
            Id = type.Id;
            Type = type;
        }
    }
}