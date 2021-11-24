using FluentMigrator;
using FluentMigrator.Postgres;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(14)]
    public class EmployeeStatusTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists employee_statuses(
                    id SERIAL PRIMARY KEY,
                    NAME TEXT NOT NULL);"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists employee_statuses;");
        }
    }
}