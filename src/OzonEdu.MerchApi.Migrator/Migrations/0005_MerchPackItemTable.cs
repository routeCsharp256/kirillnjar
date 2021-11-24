using FluentMigrator;
using FluentMigrator.Postgres;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(5)]
    public class MerchPackItemTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists merch_packs_items(
                    id SERIAL PRIMARY KEY,
                    merch_pack_id int NOT NULL,
                    merch_item_id int NOT NULL,
                    quantity int NOT NULL CHECK (quantity > 0));");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merch_packs_items;");
        }
    }
}