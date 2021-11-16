using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Infrastructure.Models;

namespace OzonEdu.MerchApi.Infrastructure.Services.Interfaces.Implementation
{
    public class StockApiMockService: IStockApiService
    {
        private IReadOnlyDictionary<long, int> _stockItems;

        public StockApiMockService(IReadOnlyDictionary<long, int> stockItems)
        {
            _stockItems = stockItems;
        }

        public StockApiMockService()
        {
            _stockItems = new Dictionary<long, int>
            {
                {1, 1000},
                {2, 1000},
                {3, 1000},
                {4,1000},
                {5, 0},
                {6, 1000},
                {7, 1000},
                {8, 1000},
                {9, 1000},
                {10,1000},
                {11, 1000},
                {12, 1000}
            };
        }

        public async Task<bool> TryReserve(IReadOnlyDictionary<MerchItemDTO, int> items, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                foreach (var merchItem in items.Keys)
                {
                    if (!_stockItems.TryGetValue(merchItem.Sku, out var stockQuantity))
                    {
                        return false;
                    }

                    if (stockQuantity < items[merchItem])
                    {
                        return false;
                    }

                }

                return true;
            }, cancellationToken);
        }
    }
}