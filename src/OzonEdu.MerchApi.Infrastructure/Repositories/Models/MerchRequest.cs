using System;

namespace OzonEdu.MerchApi.Infrastructure.Repositories.Models
{
    public class MerchRequest
    {
        public int? Id { get; set; }
        
        public int MerchPackId { get; set; }
        
        public int EmployeeId { get; set; }
        
        public int StatusTypeId { get; set; }
        
        public DateTime UpdateDate { get; set; }
        
        public int FromTypeId { get; set; }

        
    }
}