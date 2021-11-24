using FluentMigrator;
using FluentMigrator.Postgres;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(6)]
    public class MerchRequestFromType: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists merch_request_from_types(
                    id SERIAL PRIMARY KEY,
                    name TEXT NOT NULL);");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merch_request_from_types;");
        }
    }
}