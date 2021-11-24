namespace OzonEdu.MerchApi.Infrastructure.Repositories.Models
{
    public class MerchPackItem
    {
        public int Id { get; init; }
        
        public int MerchPackId { get; init; }
        
        public int MerchItemId { get; init; }
        public int Quantity{ get; init; }
    }
}