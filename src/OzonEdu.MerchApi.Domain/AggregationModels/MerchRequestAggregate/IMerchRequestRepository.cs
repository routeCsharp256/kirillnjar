using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate
{
    public interface IMerchRequestRepository : IRepository<MerchRequest>
    {
        Task<MerchRequest> Create(MerchRequest createdItem, CancellationToken cancellationToken);
        Task<MerchRequest> Update(MerchRequest updatedItem, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> GetByMerchPackAndStatus(int merchPackId, MerchRequestStatus status, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> GetByEmployeeEmailAndStatus(Email employeeEmail, MerchRequestStatus status, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> GetByEmployeeEmailAndMerchPackType(Email employeeEmail, int merchPackTypeId, CancellationToken cancellationToken);
    }
}