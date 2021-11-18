using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(12)]
    public class MerchRequestEmployeeIdIdx: ForwardOnlyMigration
    {
        public override void Up()
        {
            Create
                .Index("merch_request_employee_id_idx")
                .OnTable("merch_requests")
                .InSchema("public")
                .OnColumn("employee_id");
        }
        
    }
}