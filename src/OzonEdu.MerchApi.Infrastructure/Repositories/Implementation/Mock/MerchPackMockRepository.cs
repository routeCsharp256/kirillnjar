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
                        {new MerchItem(1, Sku.Create(1)), MerchItemsQuantity.Create(1)},
                        {new MerchItem(2, Sku.Create(2)), MerchItemsQuantity.Create(1)},
                        {new MerchItem(3, Sku.Create(3)), MerchItemsQuantity.Create(1)},
                        {new MerchItem(4, Sku.Create(4)), MerchItemsQuantity.Create(2)}
                    }),
                new(2, MerchPackType.ProbationPeriodEndingPack,
                    new Dictionary<MerchItem, MerchItemsQuantity>
                    {
                        {new MerchItem(5, Sku.Create(5)), MerchItemsQuantity.Create(1)}
                    }),
                new(3, MerchPackType.ConferenceListenerPack,
                    new Dictionary<MerchItem, MerchItemsQuantity>
                    {
                        {new MerchItem(6, Sku.Create(6)), MerchItemsQuantity.Create(1)}
                    }),
                new(4, MerchPackType.ConferenceSpeakerPack,
                    new Dictionary<MerchItem, MerchItemsQuantity>
                    {
                        {new MerchItem(7, Sku.Create(7)), MerchItemsQuantity.Create(1)},
                        {new MerchItem(8, Sku.Create(8)), MerchItemsQuantity.Create(1)}
                    }),
                new(5, MerchPackType.VeteranPack,
                    new Dictionary<MerchItem, MerchItemsQuantity>
                    {
                        {new MerchItem(9, Sku.Create(9)), MerchItemsQuantity.Create(2)},
                        {new MerchItem(10, Sku.Create(10)), MerchItemsQuantity.Create(1)},
                        {new MerchItem(11, Sku.Create(11)), MerchItemsQuantity.Create(1)},
                        {new MerchItem(12, Sku.Create(12)), MerchItemsQuantity.Create(2)}
                    }),
            };
        }
        
        public async Task<IReadOnlyList<MerchPack>> Get(int typeId, CancellationToken cancellationToken)
        {
            return await Task.Run(() 
                => _merchPacks.Where(_ => _.Id.Equals(typeId)).ToList(), cancellationToken);
        }

        public async Task<IReadOnlyList<MerchPack>> Get(IReadOnlyList<Sku> skus, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var result = new List<MerchPack>();
                foreach (var merchPack in _merchPacks)
                {
                    var merchPackSkus = merchPack.Items.Keys.Select(_ => _.Sku);
                    if (skus.Intersect(merchPackSkus).Any())
                        result.Add(merchPack);
                }

                return result;
            }, cancellationToken);
        }
    }
}