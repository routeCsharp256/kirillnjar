using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate
{
    public class MerchPackType : Enumeration
    {
        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при устройстве на работу.
        /// </summary>
        public static MerchPackType WelcomePack = new(1, nameof(WelcomePack));
        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при посещении конференции в качестве слушателя.
        /// </summary>
        public static MerchPackType ConferenceListenerPack = new(2, nameof(ConferenceListenerPack));
        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при посещении конференции в качестве спикера.
        /// </summary>
        public static MerchPackType ConferenceSpeakerPack = new(3, nameof(ConferenceSpeakerPack));
        /// <summary>
        /// Набор мерча, выдаваемый сотруднику при успешном прохождении испытательного срока.
        /// </summary>
        public static MerchPackType ProbationPeriodEndingPack = new(4, nameof(ProbationPeriodEndingPack));
        /// <summary>
        /// Набор мерча, выдаваемый сотруднику за выслугу лет.
        /// </summary>
        public static MerchPackType VeteranPack = new(5, nameof(VeteranPack));

        public MerchPackType(int id, string name) : base(id, name)
        {
        }
    }
}