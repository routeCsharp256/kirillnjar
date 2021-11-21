using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.MerchApi.Infrastructure.Repositories.Implementation.Mock
{
    public class MerchPackMockRepository : IMerchPackRepository
    {
        private IReadOnlyList<MerchPack> _merchPacks;
        
        public MerchPackMockRepository(IReadOnlyList<MerchPack> merchPacks) => _merchPacks = merchPacks; 
        
        public MerchPackMockRepository()
        {
            _merchPacks = new List<MerchPack>
            {
                new(1, MerchPackType.WelcomePack,
                    new Dictionary<MerchItem, MerchItemsQuantity>
                    {
                        {new MerchItem(1, new Sku(1)), new MerchItemsQuantity(1)},
                        {new MerchItem(2, new Sku(2)), new MerchItemsQuantity(1)},
                        {new MerchItem(3, new Sku(3)), new MerchItemsQuantity(1)},
                        {new MerchItem(4, new Sku(4)), new MerchItemsQuantity(2)}
                    }),
                new(2, MerchPackType.ProbationPeriodEndingPack,
                    new Dictionary<MerchItem, MerchItemsQuantity>
                    {
                        {new MerchItem(5, new Sku(5)), new MerchItemsQuantity(1)}
                    }),
                new(3, MerchPackType.ConferenceListenerPack,
                    new Dictionary<MerchItem, MerchItemsQuantity>
                    {
                        {new MerchItem(6, new Sku(6)), new MerchItemsQuantity(1)}
                    }),
                new(4, MerchPackType.ConferenceSpeakerPack,
                    new Dictionary<MerchItem, MerchItemsQuantity>
                    {
                        {new MerchItem(7, new Sku(7)), new MerchItemsQuantity(1)},
                        {new MerchItem(8, new Sku(8)), new MerchItemsQuantity(1)}
                    }),
                new(5, MerchPackType.VeteranPack,
                    new Dictionary<MerchItem, MerchItemsQuantity>
                    {
                        {new MerchItem(9, new Sku(9)), new MerchItemsQuantity(2)},
                        {new MerchItem(10, new Sku(10)), new MerchItemsQuantity(1)},
                        {new MerchItem(11, new Sku(11)), new MerchItemsQuantity(1)},
                        {new MerchItem(12, new Sku(12)), new MerchItemsQuantity(2)}
                    }),
            };
        }
        
        public async Task<MerchPack> Get(int typeId, CancellationToken cancellationToken)
        {
            return await Task.Run(() 
                => _merchPacks.SingleOrDefault(mp => mp.Id.Equals(typeId)), cancellationToken);
        }

        public async Task<IReadOnlyList<MerchPack>> Get(IReadOnlyList<Sku> skus, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var result = new List<MerchPack>();
                foreach (var merchPack in _merchPacks)
                {
                    var merchPackSkus = merchPack.Items.Keys.Select(mi => mi.Sku);
                    if (skus.Intersect(merchPackSkus).Any())
                        result.Add(merchPack);
                }

                return result;
            }, cancellationToken);
        }
    }
}