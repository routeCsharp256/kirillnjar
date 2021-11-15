using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Infrastructure.Models;

namespace OzonEdu.MerchApi.Infrastructure.Services.Interfaces
{
    public interface IStockApiService
    {
        public Task<bool> TryReserve(IReadOnlyDictionary<MerchItemDTO, int> items, CancellationToken cancellationToken);
    }
}