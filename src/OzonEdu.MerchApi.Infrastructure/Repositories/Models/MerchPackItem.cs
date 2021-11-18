namespace OzonEdu.MerchApi.Infrastructure.Repositories.Models
{
    public class MerchPackItem
    {
        public int? Id { get; set; }
        
        public int MerchPackId { get; set; }
        
        public int MerchItemId { get; set; }
        public int Quantity{ get; set; }
    }
}