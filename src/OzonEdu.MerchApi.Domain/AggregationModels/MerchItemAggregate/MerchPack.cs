using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate
{
    public class MerchPack : Entity, IAggregationRoot
    {
        public MerchPackType Type { get; }
        public MerchPack(MerchPackType type, IReadOnlyDictionary<MerchItem, MerchItemsQuantity> items)
        {
            Type = type;
            Items = items;
        }

        public MerchPack(int id, MerchPackType type, IReadOnlyDictionary<MerchItem, MerchItemsQuantity> items)
            : this(type, items)
        {
            Id = id;
        }
        
        public IReadOnlyDictionary<MerchItem, MerchItemsQuantity> Items { get; private set; }
    }
}