using FluentMigrator;
using FluentMigrator.Postgres;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(8)]
    public class MerchRequestTable: Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                CREATE TABLE if not exists merch_requests(
                    id SERIAL PRIMARY KEY,
                    merch_pack_id int NOT NULL,
                    employee_id int NOT NULL,
                    update_date date NOT NULL,
                    from_type_id int NOT NULL,
                    status_type_id int NOT NULL);");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merch_requests;");
        }
    }
}