using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public class MerchPackType : Enumeration
    {
        public static MerchPackType WelcomePack = new(1, nameof(WelcomePack));
        
        public static MerchPackType ConferenceListenerPack = new(2, nameof(ConferenceListenerPack));
        
        public static MerchPackType ConferenceSpeakerPack = new(3, nameof(ConferenceSpeakerPack));
        
        public static MerchPackType ProbationPeriodEndingPack = new(4, nameof(ProbationPeriodEndingPack));
        
        public static MerchPackType VeteranPack = new(5, nameof(VeteranPack));

        public MerchPackType(int id, string name) : base(id, name)
        {
        }
    }
}