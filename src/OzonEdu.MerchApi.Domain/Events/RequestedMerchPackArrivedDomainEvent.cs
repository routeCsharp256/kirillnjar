using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Exceptions;

namespace OzonEdu.MerchApi.Domain.Events
{
    public class RequestedMerchPackArrivedDomainEvent : INotification
    {
        public MerchRequest MerchRequest { get; }

        public RequestedMerchPackArrivedDomainEvent(MerchRequest merchRequest)
        {
            MerchRequest = merchRequest ?? throw new RequiredEventPropertyIsNullException(nameof(merchRequest),
                $"{nameof(RequestedMerchPackArrivedDomainEvent)} can't be created without {nameof(merchRequest)}");
        }
    }
}