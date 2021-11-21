using System.Collections.Generic;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Exceptions;

namespace OzonEdu.MerchApi.Domain.Events
{
    public class SupplyArrivedDomainEvent : INotification
    {
        public IReadOnlyDictionary<Sku, MerchItemsQuantity> Items { get; }
        
        public SupplyArrivedDomainEvent(IReadOnlyDictionary<Sku, MerchItemsQuantity> items)
        {
            Items = items ?? throw new RequiredEventPropertyIsNullException(nameof(items),
                $"{nameof(SupplyArrivedDomainEvent)} can't be created without {nameof(items)}");
        }
    }
}