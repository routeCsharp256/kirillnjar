using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public interface IMerchPackRepository : IRepository<MerchItem>
    {
        Task<MerchPack> GetByTypeId(int typeId, CancellationToken cancellationToken);

        Task<IReadOnlyList<MerchPack>> GetBySkus(IReadOnlyList<Sku> skus, CancellationToken cancellationToken);
    }
}