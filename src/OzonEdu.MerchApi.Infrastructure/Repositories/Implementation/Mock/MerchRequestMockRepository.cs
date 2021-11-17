using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;

namespace OzonEdu.MerchApi.Infrastructure.Repositories.Implementation.Mock
{
    public class MerchRequestMockRepository : IMerchRequestRepository
    {
        private IReadOnlyList<MerchRequest> _merchRequests;

        public MerchRequestMockRepository(IReadOnlyList<MerchRequest> merchRequests)
        {
            _merchRequests = merchRequests;
        }

        public MerchRequestMockRepository()
        {
            _merchRequests = new List<MerchRequest>
            {
                new(1, new Employee(
                        new Email("iivanov@mail.com"),
                        new EmployeeFullName("Ivan", "Ivanov", "Ivanovich")),
                    1,
                    new MerchRequestDateTime(DateTime.ParseExact("21.12.2020", "dd.MM.yyyy", CultureInfo.InvariantCulture)),
                    MerchRequestStatus.Done,
                    new MerchRequestFrom(MerchRequestFromType.Automatically)),
                new(2, new Employee(
                        new Email("ppetrov@mail.com"),
                        new EmployeeFullName("Petr", "Petrov", "Petrovich")),
                    2,
                    new MerchRequestDateTime(DateTime.ParseExact("22.12.2020", "dd.MM.yyyy", CultureInfo.InvariantCulture)),
                    MerchRequestStatus.AwaitingDelivery,
                    new MerchRequestFrom(MerchRequestFromType.Manually)),
                new(3, new Employee(
                        new Email("aivanova@mail.com"),
                        new EmployeeFullName("Anna", "Ivanova", "Ivanovna")),
                    5,
                    new MerchRequestDateTime(DateTime.ParseExact("23.12.2020", "dd.MM.yyyy", CultureInfo.InvariantCulture)),
                    MerchRequestStatus.Canceled,
                    new MerchRequestFrom(MerchRequestFromType.Manually))
            };
        }

        public async Task<MerchRequest> CreateAsync(MerchRequest createdItem, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
                {
                    var newId = _merchRequests.Max(_ => _.Id) + 1;
                    var newItem = new MerchRequest(newId,
                        createdItem.Employee,
                        createdItem.MerchPackId,
                        createdItem.MerchRequestDateTime,
                        createdItem.MerchRequestStatus,
                        createdItem.MerchRequestFrom);
                    _merchRequests = _merchRequests.Append(newItem).ToList();
                    return newItem;
                }, cancellationToken);
        }

        public async Task<MerchRequest> UpdateAsync(MerchRequest updatedItem, CancellationToken cancellationToken)
        {
            if (updatedItem.Id == default(long))
                throw new Exception($"for update id must have a value");
            return await Task.Run(() =>
                {
                    var itemInCollection = _merchRequests.SingleOrDefault(_ => _.Id.Equals(updatedItem.Id));
                    var merchRequestsList = _merchRequests.ToList();
                    merchRequestsList.Remove(itemInCollection);
                    _merchRequests = _merchRequests.Append(updatedItem).ToList();
                    return updatedItem;
                }, cancellationToken);
        }

        public async Task<IReadOnlyList<MerchRequest>> GetAwaitingDeliveryByMerchPackAsync(int MerchPackId, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return _merchRequests.Where(_ => _.MerchRequestStatus.Equals(MerchRequestStatus.AwaitingDelivery))
                    .ToList();
            }, cancellationToken);
        }

        public async Task<IReadOnlyList<MerchRequest>> GetAwaitingDeliveryByEmployeeEmailAsync(Email employeeEmail, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return _merchRequests.Where(_ => 
                        _.Employee.Email.Equals(employeeEmail)
                        && _.MerchRequestStatus.Equals(MerchRequestStatus.AwaitingDelivery))
                    .ToList();
            }, cancellationToken);
        }

        public async Task<IReadOnlyList<MerchRequest>> GetDoneByEmployeeEmailAsync(Email employeeEmail, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return _merchRequests.Where(_ => 
                        _.Employee.Email.Equals(employeeEmail)
                        && _.MerchRequestStatus.Equals(MerchRequestStatus.Done))
                    .ToList();
            }, cancellationToken);
        }

        public async Task<IReadOnlyList<MerchRequest>> GetByEmployeeEmailAndMerchPackTypeAsync(Email employeeEmail, int merchPackTypeId,
            CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return _merchRequests.Where(_ =>
                        _.Employee.Email.Equals(employeeEmail)
                        && _.MerchPackId.Equals(merchPackTypeId))
                    .ToList();
            }, cancellationToken);
        }

    }
}