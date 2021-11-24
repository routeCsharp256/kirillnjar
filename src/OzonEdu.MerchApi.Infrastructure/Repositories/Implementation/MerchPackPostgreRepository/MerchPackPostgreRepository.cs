using System;
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

namespace OzonEdu.MerchApi.Infrastructure.Repositories.Implementation.MerchPackPostgreRepository
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
        
        public async Task<IReadOnlyList<MerchPack>> Get(IReadOnlyList<Sku> skus, CancellationToken cancellationToken)
        {
            var parameters = new
            {
                SkuIds = skus.Select(x => x.Value).ToArray(),
            };
            
            var commandDefinition = new CommandDefinition(
                MerchPackPostgreQueries.GetBySku(),
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
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

        public async Task<IReadOnlyList<MerchPack>> Get(int typeId, CancellationToken cancellationToken)
        {
            var parameters = new
            {
                TypeId = typeId 
            };
            
            var commandDefinition = new CommandDefinition(
                MerchPackPostgreQueries.GetByTypeId(),
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var dbMerchPacks = await connection.QueryAsync<
                    Models.MerchPack, MerchPackType , Models.MerchPackItem, Models.MerchItem,
                (Models.MerchPack, MerchPackType , Models.MerchPackItem, Models.MerchItem)>
                (commandDefinition,
                    (mp, mpt, mpi, mi) => (mp, mpt, mpi, mi));
            
            var result = ToMerchPack(dbMerchPacks);
            
            foreach (var merchPack in result)
            {
                _changeTracker.Track(merchPack);
            }
            
            return result.ToList();
        }

        private static IEnumerable<MerchPack> ToMerchPack(IEnumerable<(Models.MerchPack, MerchPackType , Models.MerchPackItem, Models.MerchItem)> merchPacks)
        {
            var groupedById = merchPacks.GroupBy(item => item.Item1.Id );
            return groupedById.Select(group 
                => new MerchPack(
                    group.FirstOrDefault().Item1.Id,
                    Enumeration
                        .GetAll<MerchPackType>()
                        .FirstOrDefault(it => it.Id.Equals(group.FirstOrDefault().Item2.Id)),
                    group
                        .ToDictionary(
                            _ => new MerchItem(_.Item4.Id, Sku.Create(_.Item4.Sku)),
                            _ => MerchItemsQuantity.Create(_.Item3.Quantity))));
        }
    }
}