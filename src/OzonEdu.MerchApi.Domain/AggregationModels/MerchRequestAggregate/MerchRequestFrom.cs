using OzonEdu.MerchApi.Domain.Exceptions;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequestFrom : Entity
    {
        public MerchRequestFromType Type { get; }

        public MerchRequestFrom(MerchRequestFromType type)
        {
            if (type is null)
            {
                throw new RequiredEntityPropertyIsNullException(nameof(type), "request from type can't be null");
            }
            Id = type.Id;
            Type = type;
        }
    }
}