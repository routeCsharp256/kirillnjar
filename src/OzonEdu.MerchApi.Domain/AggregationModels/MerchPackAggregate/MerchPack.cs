using System.Collections.Generic;
using OzonEdu.MerchApi.Domain.Exceptions;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public class MerchPack : Entity, IAggregationRoot
    {
        public MerchPackType Type { get; }
        public MerchPack(MerchPackType type, IReadOnlyDictionary<MerchItem, MerchItemsQuantity> items)
        {
            Type = type ?? throw new RequiredEntityPropertyIsNullException(nameof(type), "type of merch pack can't be null");
            Items = items ?? throw new RequiredEntityPropertyIsNullException(nameof(items), "items of merch pack can't be null");
        }

        public MerchPack(int id, MerchPackType type, IReadOnlyDictionary<MerchItem, MerchItemsQuantity> items)
            : this(type, items)
        {
            Id = id;
        }
        
        public IReadOnlyDictionary<MerchItem, MerchItemsQuantity> Items { get; }
    }
}