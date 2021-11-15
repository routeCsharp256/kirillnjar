using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;

namespace OzonEdu.MerchApi.Domain.Events
{
    public class MerchPackReservationFailureDomainEvent : INotification
    {
        public MerchRequest MerchRequest { get; }

        public MerchPackReservationFailureDomainEvent(MerchRequest merchRequest)
        {
            MerchRequest = merchRequest;
        }
    }
}