using FluentMigrator;
using FluentMigrator.Postgres;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(4)]
    public class MerchPackTypeTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists merch_pack_types(
                    id SERIAL PRIMARY KEY,
                    name text NOT NULL);");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merch_pack_types;");
        }
    }
}