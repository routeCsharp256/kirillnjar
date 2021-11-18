using System;

namespace OzonEdu.MerchApi.Infrastructure.Repositories.Models
{
    public class MerchPack
    {
        public int? Id { get; set; }
        
        public int TypeId { get; set; }
        
        public DateTime? EndDate { get; set; }
    }
}