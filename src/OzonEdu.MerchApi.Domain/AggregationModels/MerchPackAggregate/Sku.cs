using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Exceptions.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public class Sku : ValueObject
    {
        public long Value { get; }
        
        private Sku(long sku)
        {
            Value = sku;
        }
        
        public static Sku Create(long sku)
        {
            if (sku <= 0)
                throw new InvalidSkuException("Sku value must be positive");
            return new Sku(sku);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}