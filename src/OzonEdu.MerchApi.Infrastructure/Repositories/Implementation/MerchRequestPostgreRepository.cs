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

namespace OzonEdu.MerchApi.Infrastructure.Repositories.Implementation
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
            const string sql = @"
                with inserted_employee as (select id from employees  where email = @Email), 
                     inserting_employee as (insert into employees (first_name, last_name, middle_name, email, status_id)
                    values (@FirstName, @LastName, @MiddleName, @Email, @StatusId) on conflict do nothing returning id
                ), employee_id AS ( select id from inserted_employee union all
                                    select id from inserting_employee)
                insert into merch_requests (merch_pack_id, employee_id, update_date, from_type_id, status_type_id)
                values (@MerchPackId, (select employee_id.id from employee_id), @UpdateDate, @FromTypeId, @StatusTypeId)
                returning id as merch_id";
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
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var id = await connection.QueryAsync<int>(commandDefinition);
            var result = await GetByIdAsync(id.Single(), cancellationToken);
            _changeTracker.Track(result);
            return result;
            
        }

        public async Task<MerchRequest> Update(MerchRequest updatedItem, CancellationToken cancellationToken)
        {
            const string sql = @"
                update employees
                set status_id = @StatusId
                where employees.id = @EmployeeId;
                update merch_requests
                set update_date = @UpdateDate,
                    status_type_id = @StatusTypeId
                where merch_requests.id = @MerchRequestId;";
            
            var parameters = new
            {
                EmployeeId = updatedItem.Employee.Id,
                StatusId = updatedItem.Employee.Status.Id,
                MerchRequestId = updatedItem.Id,
                UpdateDate = updatedItem.MerchRequestDateTime.Value,
                StatusTypeId = updatedItem.MerchRequestStatus.Id,
            };
            
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            var result = await GetByIdAsync(updatedItem.Id, cancellationToken);
            _changeTracker.Track(result);
            return result;
        }

        public async Task<IReadOnlyList<MerchRequest>> Get(int merchPackId, MerchRequestStatus status, CancellationToken cancellationToken)
        {
            const string sql = @"
                select mr. id, mr.merch_pack_id as MerchPackId, mr.employee_id as EmployeeId, mr.status_type_id as StatusTypeId, 
                       mr.update_date as UpdateDate, mr.from_type_id as FromTypeId,
                       emp.id, emp.first_name as FirstName, emp.last_name as LastName, emp.middle_name as MiddleName, 
                       emp.email as Email, emp.status_id as StatusId
                from merch_requests mr
                join employees emp on mr.employee_id = emp.id
                where mr.merch_pack_id = @MerchPackId,
                and mr.status_type_id = @MerchRequestStatusId;";
            
            var parameters = new
            {
                MerchPackId = merchPackId,
                MerchRequestStatusId = status.Id
            };
            
            var commandDefinition = new CommandDefinition(
                sql,
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
            const string sql = @"
                select mr. id, mr.merch_pack_id as MerchPackId, mr.employee_id as EmployeeId, mr.status_type_id as StatusTypeId, 
                       mr.update_date as UpdateDate, mr.from_type_id as FromTypeId,
                       emp.id, emp.first_name as FirstName, emp.last_name as LastName, emp.middle_name as MiddleName, 
                       emp.email as Email, emp.status_id as StatusId
                from merch_requests mr
                join employees emp on mr.employee_id = emp.id
                where emp.email = @EmployeeEmail
                and mr.status_type_id = @MerchRequestStatusId;";
            
            var parameters = new
            {
                EmployeeEmail = employeeEmail.Value,
                MerchRequestStatusId = status.Id
            };
            
            var commandDefinition = new CommandDefinition(
                sql,
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
            const string sql = @"
                select mr. id, mr.merch_pack_id as MerchPackId, mr.employee_id as EmployeeId, mr.status_type_id as StatusTypeId, 
                       mr.update_date as UpdateDate, mr.from_type_id as FromTypeId,
                       emp.id, emp.first_name as FirstName, emp.last_name as LastName, emp.middle_name as MiddleName, 
                       emp.email as Email, emp.status_id as StatusId
                from merch_requests mr
                join employees emp on mr.employee_id = emp.id
                where emp.email = @EmployeeEmail
                and mr.merch_pack_id = @MerchPackId;";
            
            var parameters = new
            {
                EmployeeEmail = employeeEmail.Value,
                MerchPackId = merchPackTypeId
            };
            
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var dbMerchRequests1 = await connection.QueryAsync(commandDefinition);
            var dbMerchRequests = await connection.QueryAsync<Models.MerchRequest, Models.Employee, MerchRequest>
            (commandDefinition,ToMerchRequest);
            return dbMerchRequests.ToList();
        }

        private async Task<MerchRequest> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            const string sql = @"
                select mr. id, mr.merch_pack_id as MerchPackId, mr.employee_id as EmployeeId, mr.status_type_id as StatusTypeId, 
                       mr.update_date as UpdateDate, mr.from_type_id as FromTypeId,
                       emp.id, emp.first_name as FirstName, emp.last_name as LastName, emp.middle_name as MiddleName, 
                       emp.email as Email, emp.status_id as StatusId
                from merch_requests mr
                join employees emp on mr.employee_id = emp.id
                where mr.id = @MerchRequestId;";
            
            var parameters = new
            {
                MerchRequestId = id,
            };
            
            var commandDefinition = new CommandDefinition(
                sql,
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