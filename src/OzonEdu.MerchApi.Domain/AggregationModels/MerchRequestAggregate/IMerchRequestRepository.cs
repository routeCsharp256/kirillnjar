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
        Task<IReadOnlyList<MerchRequest>> GetByMerchPackAndStatusAsync(int merchPackId, MerchRequestStatus status, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> GetByEmployeeEmailAndStatusAsync(Email employeeEmail, MerchRequestStatus status, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> GetByEmployeeEmailAndMerchPackTypeAsync(Email employeeEmail, int merchPackTypeId, CancellationToken cancellationToken);
    }
}