using System;
using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Exceptions.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequestDateTime : ValueObject
    {
        public DateTime Value { get; } 
        
        public MerchRequestDateTime(DateTime value)
        {
            if (value <= DateTime.UtcNow)
            {
                throw new MerchRequestDateException("date cannot be greater than current");
            }
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}