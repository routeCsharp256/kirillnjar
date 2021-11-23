using System;

namespace OzonEdu.MerchApi.Infrastructure.Repositories.Models
{
    public class MerchPack
    {
        public int Id { get; init; }
        
        public int TypeId { get; init; }
        
        public DateTime? EndDate { get; init; }
    }
}