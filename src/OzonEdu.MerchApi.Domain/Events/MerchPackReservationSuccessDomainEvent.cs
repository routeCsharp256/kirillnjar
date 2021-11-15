using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;

namespace OzonEdu.MerchApi.Domain.Events
{
    public class MerchPackReservationSuccessDomainEvent : INotification
    {
        public MerchRequest MerchRequest { get; }

        public MerchPackReservationSuccessDomainEvent(MerchRequest merchRequest)
        {
            MerchRequest = merchRequest;
        }
    }
}