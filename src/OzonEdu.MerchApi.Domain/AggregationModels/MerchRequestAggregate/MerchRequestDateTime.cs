using System;
using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequestDateTime : ValueObject
    {
        public DateTime Value { get; } 
        
        private MerchRequestDateTime(DateTime value)
        {
            Value = value;
        }
        
        
        public static MerchRequestDateTime Create(DateTime value)
        {
            return new MerchRequestDateTime(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}