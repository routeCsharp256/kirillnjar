using OzonEdu.MerchApi.Domain.Exceptions;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public class MerchItem : Entity
    {
        public MerchItem(Sku sku)
        {
            Sku = sku ?? throw new RequiredEntityPropertyIsNullException(nameof(sku), "Sku of merch item can't be null");
        }

        public MerchItem(int id, Sku sku)
            : this(sku)
        {
            Id = id;
        }

        public Sku Sku { get; private set; }
    }
}