namespace OzonEdu.MerchApi.Infrastructure.Repositories.Implementation.MerchRequestPostgreRepository
{
    public static class MerchRequestPostgreQueries
    {
        public static string Create() => @"
                with inserted_employee as (select id from employees  where email = @Email), 
                     inserting_employee as (insert into employees (first_name, last_name, middle_name, email, status_id)
                    values (@FirstName, @LastName, @MiddleName, @Email, @StatusId) on conflict do nothing returning id
                ), employee_id AS ( select id from inserted_employee union all
                                    select id from inserting_employee)
                insert into merch_requests (merch_pack_id, employee_id, update_date, from_type_id, status_type_id)
                values (@MerchPackId, (select employee_id.id from employee_id), @UpdateDate, @FromTypeId, @StatusTypeId)
                returning id as merch_id;";

        public static string Update() => @"
                update employees
                set status_id = @StatusId
                where employees.id = @EmployeeId;
                update merch_requests
                set update_date = @UpdateDate,
                    status_type_id = @StatusTypeId
                where merch_requests.id = @MerchRequestId;";

        public static string GetByMerchPackIdAndStatus() => @"
                select mr. id, mr.merch_pack_id as MerchPackId, mr.employee_id as EmployeeId, mr.status_type_id as StatusTypeId, 
                       mr.update_date as UpdateDate, mr.from_type_id as FromTypeId,
                       emp.id, emp.first_name as FirstName, emp.last_name as LastName, emp.middle_name as MiddleName, 
                       emp.email as Email, emp.status_id as StatusId
                from merch_requests mr
                join employees emp on mr.employee_id = emp.id
                where mr.merch_pack_id = @MerchPackId,
                and mr.status_type_id = @MerchRequestStatusId;";
        
        public static string GetByEmployeeEmailAndStatus() => @"
                select mr. id, mr.merch_pack_id as MerchPackId, mr.employee_id as EmployeeId, mr.status_type_id as StatusTypeId, 
                       mr.update_date as UpdateDate, mr.from_type_id as FromTypeId,
                       emp.id, emp.first_name as FirstName, emp.last_name as LastName, emp.middle_name as MiddleName, 
                       emp.email as Email, emp.status_id as StatusId
                from merch_requests mr
                join employees emp on mr.employee_id = emp.id
                where emp.email = @EmployeeEmail
                and mr.status_type_id = @MerchRequestStatusId;";

        public static string GetByEmployeeEmailAndMerchPackId() => @"
                select mr. id, mr.merch_pack_id as MerchPackId, mr.employee_id as EmployeeId, mr.status_type_id as StatusTypeId, 
                       mr.update_date as UpdateDate, mr.from_type_id as FromTypeId,
                       emp.id, emp.first_name as FirstName, emp.last_name as LastName, emp.middle_name as MiddleName, 
                       emp.email as Email, emp.status_id as StatusId
                from merch_requests mr
                join employees emp on mr.employee_id = emp.id
                where emp.email = @EmployeeEmail
                and mr.merch_pack_id = @MerchPackId;";
        
        
        public static string GetById() => @"
                select mr. id, mr.merch_pack_id as MerchPackId, mr.employee_id as EmployeeId, mr.status_type_id as StatusTypeId, 
                       mr.update_date as UpdateDate, mr.from_type_id as FromTypeId,
                       emp.id, emp.first_name as FirstName, emp.last_name as LastName, emp.middle_name as MiddleName, 
                       emp.email as Email, emp.status_id as StatusId
                from merch_requests mr
                join employees emp on mr.employee_id = emp.id
                where mr.id = @MerchRequestId;";
    }
}