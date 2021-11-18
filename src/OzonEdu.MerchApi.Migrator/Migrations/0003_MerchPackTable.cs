using FluentMigrator;
using FluentMigrator.Postgres;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(3)]
    public class MerchPackTable: Migration
    {
        public override void Up()
        {
            /*
             * end_date - если необходимо обновить состав пака,
             * в старом устанавливается end_date = NOW()
             * и создается новый пак с end_date = NULL
             */
            Execute.Sql(@"
                CREATE TABLE if not exists merch_packs(
                    id SERIAL PRIMARY KEY,
                    type_id int NOT NULL,
                    end_date date );");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merch_packs;");
        }
    }
}