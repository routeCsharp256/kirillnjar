using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate
{
    public class MerchItem : Entity
    {
        public MerchItem(Sku sku)
        {
            Sku = sku;
        }

        public MerchItem(int id, Sku sku)
            : this(sku)
        {
            Id = id;
        }

        public Sku Sku { get; private set; }
    }
}