using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Exceptions;

namespace OzonEdu.MerchApi.Domain.Events
{
    public class MerchPackReservationFailureDomainEvent : INotification
    {
        public MerchRequest MerchRequest { get; }

        public MerchPackReservationFailureDomainEvent(MerchRequest merchRequest)
        {
            MerchRequest = merchRequest ?? throw new RequiredEventPropertyIsNullException(nameof(merchRequest),
                $"{nameof(MerchPackReservationFailureDomainEvent)} can't be created without {nameof(merchRequest)}");
        }
    }
}