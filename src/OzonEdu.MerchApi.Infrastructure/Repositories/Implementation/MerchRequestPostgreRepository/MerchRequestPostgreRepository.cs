using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Models;
using OzonEdu.MerchApi.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.MerchApi.Infrastructure.Repositories.Implementation.MerchRequestPostgreRepository
{
    public class MerchRequestPostgreRepository : IMerchRequestRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchRequestPostgreRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<MerchRequest> Create(MerchRequest createdItem, CancellationToken cancellationToken)
        {
            var parameters = new
            {
                FirstName = createdItem.Employee.Name.FirstName.Value,
                LastName = createdItem.Employee.Name.LastName.Value,
                MiddleName = createdItem.Employee.Name.MiddleName.Value,
                Email = createdItem.Employee.Email.Value,
                StatusId = createdItem.Employee.Status.Id,
                MerchPackId = createdItem.MerchPackId,
                UpdateDate = createdItem.MerchRequestDateTime.Value,
                FromTypeId = createdItem.MerchRequestFrom.Id,
                StatusTypeId = createdItem.MerchRequestStatus.Id,
            };
            
            var commandDefinition = new CommandDefinition(
                MerchRequestPostgreQueries.Create(),
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var id = await connection.QueryAsync<int>(commandDefinition);
            var result = await Get(id.Single(), cancellationToken);
            _changeTracker.Track(result);
            return result;
            
        }

        public async Task<MerchRequest> Update(MerchRequest updatedItem, CancellationToken cancellationToken)
        {
            var parameters = new
            {
                EmployeeId = updatedItem.Employee.Id,
                StatusId = updatedItem.Employee.Status.Id,
                MerchRequestId = updatedItem.Id,
                UpdateDate = updatedItem.MerchRequestDateTime.Value,
                StatusTypeId = updatedItem.MerchRequestStatus.Id,
            };
            
            var commandDefinition = new CommandDefinition(
                MerchRequestPostgreQueries.Update(),
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            var result = await Get(updatedItem.Id, cancellationToken);
            _changeTracker.Track(result);
            return result;
        }

        public async Task<IReadOnlyList<MerchRequest>> Get(int merchPackId, MerchRequestStatus status, CancellationToken cancellationToken)
        {
            var parameters = new
            {
                MerchPackId = merchPackId,
                MerchRequestStatusId = status.Id
            };
            
            var commandDefinition = new CommandDefinition(
                MerchRequestPostgreQueries.GetByMerchPackIdAndStatus(),
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var dbMerchRequests = await connection.QueryAsync<Models.MerchRequest, Models.Employee, MerchRequest>
            (commandDefinition, ToMerchRequest);
            return dbMerchRequests.ToList();
        }

        public async Task<IReadOnlyList<MerchRequest>> Get(Email employeeEmail, MerchRequestStatus status,
            CancellationToken cancellationToken)
        {
            var parameters = new
            {
                EmployeeEmail = employeeEmail.Value,
                MerchRequestStatusId = status.Id
            };
            
            var commandDefinition = new CommandDefinition(
                MerchRequestPostgreQueries.GetByEmployeeEmailAndStatus(),
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var dbMerchRequests = await connection.QueryAsync<Models.MerchRequest, Models.Employee, MerchRequest>
                (commandDefinition, ToMerchRequest);
            return dbMerchRequests.ToList();
        }



        public async Task<IReadOnlyList<MerchRequest>> Get(Email employeeEmail, int merchPackTypeId,
            CancellationToken cancellationToken)
        {
            var parameters = new
            {
                EmployeeEmail = employeeEmail.Value,
                MerchPackId = merchPackTypeId
            };
            
            var commandDefinition = new CommandDefinition(
                MerchRequestPostgreQueries.GetByEmployeeEmailAndMerchPackId(),
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var dbMerchRequests = await connection.QueryAsync<Models.MerchRequest, Models.Employee, MerchRequest>
            (commandDefinition,ToMerchRequest);
            return dbMerchRequests.ToList();
        }

        private async Task<MerchRequest> Get(int id, CancellationToken cancellationToken)
        {
            var parameters = new
            {
                MerchRequestId = id,
            };
            
            var commandDefinition = new CommandDefinition(
                MerchRequestPostgreQueries.GetById(),
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var dbMerchRequests = await connection.QueryAsync<Models.MerchRequest, Models.Employee, MerchRequest>
            (commandDefinition, ToMerchRequest);
            return dbMerchRequests.Single();
        }


        private MerchRequest ToMerchRequest(Models.MerchRequest mr, Models.Employee emp) 
            => new MerchRequest(
                mr.Id.Value,
                new Employee(
                    emp.Id.Value,
                    Email.Create(emp.Email),
                    FullName.Create(emp.LastName, 
                        emp.FirstName,
                        emp.MiddleName),
                    Enumeration
                        .GetAll<EmployeeStatus>()
                        .FirstOrDefault(it => it.Id.Equals(emp.StatusId))),
                mr.MerchPackId,
                MerchRequestDateTime.Create(mr.UpdateDate),
                Enumeration
                    .GetAll<MerchRequestStatus>()
                    .FirstOrDefault(it => it.Id.Equals(mr.StatusTypeId)),
                new MerchRequestFrom( Enumeration
                    .GetAll<MerchRequestFromType>()
                    .FirstOrDefault(it => it.Id.Equals(mr.FromTypeId))));
    }
}