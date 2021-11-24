using FluentMigrator;

namespace OzonEdu.MerchApi.Migrator.Migrations
{
    [Migration(15)]
    public class FillDictionaries:ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                INSERT INTO employee_statuses (id, name)
                VALUES 
                    (1, 'Work'),
                    (2, 'Dismissed')
                ON CONFLICT DO NOTHING
            ");
            Execute.Sql(@"
                INSERT INTO merch_pack_types (id, name)
                VALUES 
                    (1, 'WelcomePack'),
                    (2, 'ConferenceListenerPack'),
                    (3, 'ConferenceSpeakerPack'),
                    (4, 'ProbationPeriodEndingPack'),
                    (5, 'VeteranPack')
                ON CONFLICT DO NOTHING
            ");
            
            Execute.Sql(@"
                INSERT INTO merch_request_from_types (id, name)
                VALUES 
                    (1, 'Manually'),
                    (2, 'Automatically')
                ON CONFLICT DO NOTHING
            ");
            
            Execute.Sql(@"
                INSERT INTO merch_request_status_types (id, name)
                VALUES 
                    (1, 'InWork'),
                    (2, 'AwaitingDelivery'),
                    (3, 'Canceled'),
                    (4, 'Done')
                ON CONFLICT DO NOTHING
            ");
        }
    }
}