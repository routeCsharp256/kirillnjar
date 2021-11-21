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
        Task<IReadOnlyList<MerchRequest>> Get(int merchPackId, MerchRequestStatus status, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> Get(Email employeeEmail, MerchRequestStatus status, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> Get(Email employeeEmail, int merchPackTypeId, CancellationToken cancellationToken);
    }
}