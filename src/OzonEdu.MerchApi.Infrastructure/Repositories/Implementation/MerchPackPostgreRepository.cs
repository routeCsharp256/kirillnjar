using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Exceptions.MerchPackAggregate;
using OzonEdu.MerchApi.Domain.Models;
using OzonEdu.MerchApi.Infrastructure.Repositories.Infrastructure.Interfaces;
using MerchPack = OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate.MerchPack;
using Sku = OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate.Sku;

namespace OzonEdu.MerchApi.Infrastructure.Repositories.Implementation
{
    public class MerchPackPostgreRepository : IMerchPackRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchPackPostgreRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }
        
        public async Task<IReadOnlyList<MerchPack>> GetBySkusAsync(IReadOnlyList<Sku> skus, CancellationToken cancellationToken)
        {
            const string sql = @"
                select mp.id, mp.end_date, mp.type_id,
                       mpt.id, mpt.name,
                       mpi.id, mpi.merch_item_id, mpi.merch_pack_id, mpi.quantity,
                       mi.id, mi.sku
                from merch_packs mp
                join merch_pack_types mpt on mp.type_id = mpt.id
                join merch_packs_items mpi on mp.id = mpi.merch_pack_id
                join merch_items mi on mpi.merch_item_id = mi.id
                where mpi.merch_pack_id = ANY(
                    select mpi_in.merch_pack_id
                    from merch_packs_items mpi_in
                    join merch_items mi_in on mpi_in.merch_item_id = mi_in.id
                    where mi_in.sku = ANY(@SkuIds))
                AND end_date is null";
            
            var parameters = new
            {
                SkuIds = skus.Select(x => x.Value).ToArray(),
            };
            
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            // TODO: Dictionary into merch pack model
            var dbMerchPacks = await connection.QueryAsync<
                    Models.MerchPack, MerchPackType , Models.MerchPackItem, Models.MerchItem,
                (Models.MerchPack, MerchPackType , Models.MerchPackItem, Models.MerchItem)>
                (commandDefinition,
                    (mp, mpt, mpi, mi) => (mp, mpt, mpi, mi));
            
            if (!dbMerchPacks.Any())
                throw new MerchPackNotFoundException($"Merch packs not found");
            
            var result = ToMerchPack(dbMerchPacks);
            
            foreach (var merchPack in result)
            {
                _changeTracker.Track(merchPack);
            }
            
            return result.ToList();
        }

        public async Task<MerchPack> GetByTypeIdAsync(int typeId, CancellationToken cancellationToken)
        {
            const string sql = @"
                select mp.id, mp.end_date, mp.type_id,
                       mpt.id, mpt.name,
                       mpi.id, mpi.merch_item_id, mpi.merch_pack_id, mpi.quantity,
                       mi.id, mi.sku
                from merch_packs mp
                join merch_pack_types mpt on mp.type_id = mpt.id
                join merch_packs_items mpi on mp.id = mpi.merch_pack_id
                join merch_items mi on mpi.merch_item_id = mi.id
                where mp.type_id = @TypeId
                AND end_date is null;";

            var parameters = new
            {
                TypeId = typeId 
            };
            
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            // TODO: Dictionary into merch pack model
            var dbMerchPacks = await connection.QueryAsync<
                    Models.MerchPack, MerchPackType , Models.MerchPackItem, Models.MerchItem,
                (Models.MerchPack, MerchPackType , Models.MerchPackItem, Models.MerchItem)>
                (commandDefinition,
                    (mp, mpt, mpi, mi) => (mp, mpt, mpi, mi));

            if (!dbMerchPacks.Any())
                throw new MerchPackNotFoundException($"Merch pack with id {typeId} not found");

            var result = ToMerchPack(dbMerchPacks).Single();
            
            _changeTracker.Track(result);
            return result;
        }

        private static IEnumerable<MerchPack> ToMerchPack(IEnumerable<(Models.MerchPack, MerchPackType , Models.MerchPackItem, Models.MerchItem)> merchPacks)
        {
            var groupedById = merchPacks.GroupBy(item => item.Item1.Id.Value );
            return groupedById.Select(group 
                => new MerchPack(
                    group.FirstOrDefault().Item1.Id.Value,
                    Enumeration
                        .GetAll<MerchPackType>()
                        .FirstOrDefault(it => it.Id.Equals(group.FirstOrDefault().Item2.Id)),
                    group
                        .ToDictionary(
                            _ => new MerchItem(_.Item4.Id, new Sku(_.Item4.Sku)),
                            _ => new MerchItemsQuantity(_.Item3.Quantity))));
        }
    }
}