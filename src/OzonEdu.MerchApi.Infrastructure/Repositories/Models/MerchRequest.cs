using System;

namespace OzonEdu.MerchApi.Infrastructure.Repositories.Models
{
    public class MerchRequest
    {
        public int Id { get; init; }
        
        public int MerchPackId { get; init; }
        
        public int EmployeeId { get; init; }
        
        public int StatusTypeId { get; init; }
        
        public DateTime UpdateDate { get; init; }
        
        public int FromTypeId { get; init; }

        
    }
}