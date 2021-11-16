using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Exceptions.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public class MerchItemsQuantity : ValueObject
    {
        public MerchItemsQuantity(int value)
        {
            if (value <= 0)
                throw new InvalidQuantityException($"Merch items quantity can't be lower then 1");
            Value = value;
        }

        public int Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}