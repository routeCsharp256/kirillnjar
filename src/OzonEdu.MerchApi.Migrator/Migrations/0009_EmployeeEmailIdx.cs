using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(9)]
    public class EmployeeEmailIdx: ForwardOnlyMigration
    {
        public override void Up()
        {
            Create
                .Index("employee_email_idx")
                .OnTable("employees")
                .InSchema("public")
                .OnColumn("email");
        }
        
    }
}