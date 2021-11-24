using FluentMigrator;
using FluentMigrator.Postgres;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(7)]
    public class MerchRequestStatusType: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists merch_request_status_types(
                    id SERIAL PRIMARY KEY,
                    name TEXT NOT NULL);");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merch_request_status_types;");
        }
    }
}