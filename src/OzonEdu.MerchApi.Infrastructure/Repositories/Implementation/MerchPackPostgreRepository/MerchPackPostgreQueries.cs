namespace OzonEdu.MerchApi.Infrastructure.Repositories.Implementation.MerchPackPostgreRepository
{
    public static class MerchPackPostgreQueries
    {
        public static string GetBySku() => @"
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
                AND end_date is null;";

        public static string GetByTypeId() => @"
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

    }
}