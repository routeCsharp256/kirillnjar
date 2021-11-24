using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(11)]
    public class MerchItemSkuIdx: ForwardOnlyMigration
    {
        public override void Up()
        {
            Create
                .Index("merch_items_sku_idx")
                .OnTable("merch_items")
                .InSchema("public")
                .OnColumn("sku");
        }
        
    }
}