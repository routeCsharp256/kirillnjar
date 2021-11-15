using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;

namespace OzonEdu.MerchApi.Domain.Events
{
    public class RequestedMerchPackArrivedDomainEvent : INotification
    {
        public MerchRequest MerchRequest { get; }

        public RequestedMerchPackArrivedDomainEvent(MerchRequest merchRequest)
        {
            MerchRequest = merchRequest;
        }
    }
}