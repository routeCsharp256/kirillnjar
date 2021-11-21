using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Exceptions;

namespace OzonEdu.MerchApi.Domain.Events
{
    public class MerchPackReservationSuccessDomainEvent : INotification
    {
        public MerchRequest MerchRequest { get; }

        public MerchPackReservationSuccessDomainEvent(MerchRequest merchRequest)
        {
            MerchRequest = merchRequest ?? throw new RequiredEventPropertyIsNullException(nameof(merchRequest),
                $"{nameof(MerchPackReservationSuccessDomainEvent)} can't be created without {nameof(merchRequest)}");
        }
    }
}