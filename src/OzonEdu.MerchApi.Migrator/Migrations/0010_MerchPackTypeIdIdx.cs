using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(10)]
    public class MerchPackTypeIdIdx: ForwardOnlyMigration
    {
        public override void Up()
        {
            Create
                .Index("merch_packs_type_id_idx")
                .OnTable("merch_packs")
                .InSchema("public")
                .OnColumn("type_id").Ascending()
                .OnColumn("end_date").Ascending();
        }
        
    }
}