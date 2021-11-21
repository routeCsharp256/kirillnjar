using System.Collections.Generic;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Exceptions;

namespace OzonEdu.MerchApi.Domain.Events
{
    public class SupplyArrivedWithStockItemsDomainEvent : INotification
    {
        public IReadOnlyDictionary<Sku, MerchItemsQuantity> Items { get; }
        
        public SupplyArrivedWithStockItemsDomainEvent(IReadOnlyDictionary<Sku, MerchItemsQuantity> items)
        {
            Items = items ?? throw new RequiredEventPropertyIsNullException(nameof(items),
                $"{nameof(SupplyArrivedWithStockItemsDomainEvent)} can't be created without {nameof(items)}");
        }
    }
}