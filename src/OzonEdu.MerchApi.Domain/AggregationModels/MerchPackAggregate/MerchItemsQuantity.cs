using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Exceptions.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public class MerchItemsQuantity : ValueObject
    {
        private MerchItemsQuantity(int value)
        {
            Value = value;
        }

        public static MerchItemsQuantity Create(int quantity)
        {
            if (quantity <= 0)
                throw new InvalidQuantityException($"Merch items quantity can't be lower then 1");
            return new MerchItemsQuantity(quantity);
        }
        
        public int Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}