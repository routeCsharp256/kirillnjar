using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public interface IMerchPackRepository : IRepository<MerchPack>
    {
        Task<IReadOnlyList<MerchPack>> Get(int typeId, CancellationToken cancellationToken);

        Task<IReadOnlyList<MerchPack>> Get(IReadOnlyList<Sku> skus, CancellationToken cancellationToken);
    }
}