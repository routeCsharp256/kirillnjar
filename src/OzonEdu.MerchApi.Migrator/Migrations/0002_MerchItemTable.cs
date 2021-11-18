using FluentMigrator;
using FluentMigrator.Postgres;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(2)]
    public class MerchItemTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists merch_items(
                    id SERIAL PRIMARY KEY,
                    sku bigint NOT NULL);");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merch_items;");
        }
    }
}