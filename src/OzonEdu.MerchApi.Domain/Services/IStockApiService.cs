using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.MerchApi.Domain.Services
{
    public interface IStockApiService
    {
        public Task<bool> TryReserve(MerchPack merchPack, CancellationToken cancellationToken);
    }
}