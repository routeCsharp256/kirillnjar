using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequestStatus : Enumeration
    {
        public static MerchRequestStatus InWork  = new(1, nameof(InWork));
        public static MerchRequestStatus AwaitingDelivery  = new(2, nameof(AwaitingDelivery));
        public static MerchRequestStatus Canceled  = new(3, nameof(Canceled));
        public static MerchRequestStatus Done = new(4, nameof(Done));

        public MerchRequestStatus(int id, string name) : base(id, name)
        {
        }
    }
}