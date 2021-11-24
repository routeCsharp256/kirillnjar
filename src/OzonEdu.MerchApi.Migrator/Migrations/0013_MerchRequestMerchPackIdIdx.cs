using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(13)]
    public class MerchRequestMerchPackIdIdx: ForwardOnlyMigration
    {
        public override void Up()
        {
            Create
                .Index("merch_request_merch_pack_id_idx")
                .OnTable("merch_requests")
                .InSchema("public")
                .OnColumn("merch_pack_id");
        }
        
    }
}