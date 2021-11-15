using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate
{
    public interface IMerchRequestRepository : IRepository<MerchRequest>
    {
        Task<IReadOnlyList<MerchRequest>> GetAwaitingDeliveryByMerchPackAsync(int MerchPackId, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> GetNotDoneByEmployeeEmailAsync(Email employeeEmail, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> GetDoneByEmployeeEmailAsync(Email employeeEmail, CancellationToken cancellationToken);
        Task<IReadOnlyList<MerchRequest>> GetByEmployeeEmailAndMerchPackTypeAsync(Email employeeEmail, int merchPackTypeId, CancellationToken cancellationToken);
        
    }
}