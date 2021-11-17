using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate
{
    public interface IMerchRequestRepository : IRepository<MerchRequest>
    {
        Task<MerchRequest> CreateAsync(MerchRequest createdItem, CancellationToken cancellationToken);
        Task<MerchRequest> UpdateAsync(MerchRequest updatedItem, CancellationToken cancellationToken);
        Task<MerchRequest> CreateOrUpdateAsync(MerchRequest request, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> GetAwaitingDeliveryByMerchPackAsync(int merchPackId, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> GetDoneByEmployeeEmailAsync(Email employeeEmail, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> GetByEmployeeEmailAndMerchPackTypeAsync(Email employeeEmail, int merchPackTypeId, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> GetAwaitingDeliveryByEmployeeEmailAsync(Email employeeEmail, CancellationToken cancellationToken);
    }
}