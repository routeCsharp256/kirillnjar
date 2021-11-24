using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(16)]
    public class FillMerchPacks:ForwardOnlyMigration
    {
        public override void Up()
        {
            
            Execute.Sql(@"
                INSERT INTO merch_packs (id, type_id)
                VALUES 
                    (1, 1),
                    (2, 2),
                    (3, 3),
                    (4, 4),
                    (5, 5)
                ON CONFLICT DO NOTHING
            ");
            
            Execute.Sql(@"
                INSERT INTO merch_items (id, sku)
                VALUES 
                    (1, 1),
                    (2, 2),
                    (3, 3),
                    (4, 4),
                    (5, 5),
                    (6, 6),
                    (7, 7),
                    (8, 8),
                    (9, 9),
                    (10, 10),
                    (11, 11),
                    (12, 12)
                ON CONFLICT DO NOTHING
            ");
            
            Execute.Sql(@"
                INSERT INTO merch_packs_items (id, merch_pack_id, merch_item_id, quantity)
                VALUES 
                    (1, 1, 1, 1),
                    (2, 1, 2, 1),
                    (3, 1, 3, 1),
                    (4, 1, 4, 2),
                    (5, 2, 5, 1),
                    (6, 3, 6, 1),
                    (7, 4, 7, 1),
                    (8, 4, 8, 1),
                    (9, 5, 9, 2),
                    (10, 5, 10, 1),
                    (11, 5, 11, 1),
                    (12, 5, 12, 1)
                ON CONFLICT DO NOTHING
            ");
        }
    }
}