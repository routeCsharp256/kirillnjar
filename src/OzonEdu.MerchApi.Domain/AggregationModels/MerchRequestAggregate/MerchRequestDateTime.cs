using System;
using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequestDateTime : ValueObject
    {
        public DateTime Value { get; } 
        
        public MerchRequestDateTime(DateTime value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}