using System.Collections.Generic;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;

namespace OzonEdu.MerchApi.Domain.Events
{
    public class SupplyArrivedWithStockItemsDomainEvent : INotification
    {
        public SupplyArrivedWithStockItemsDomainEvent(IReadOnlyDictionary<Sku, MerchItemsQuantity> items)
        {
            Items = items;
        }

        public IReadOnlyDictionary<Sku, MerchItemsQuantity> Items { get; private set; }
    }
}