using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate
{
    public interface IMerchPackRepository : IRepository<MerchItem>
    {
        Task<MerchPack> GetByTypeIdAsync(int TypeId, CancellationToken cancellationToken);

        Task<IReadOnlyList<MerchPack>> GetBySkusAsync(IReadOnlyList<Sku> skus, CancellationToken cancellationToken);
    }
}