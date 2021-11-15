namespace OzonEdu.MerchApi.Infrastructure.Models
{
    public class MerchRequestDTO
    {
        public int Id { get; set; }
        public EmployeeDTO Employee { get; set; }
        public MerchPackDTO MerchPack { get; set; }
        public int MerchRequestFrom { get; set; }
        public int MerchRequestStatus { get; set; }
        public int MerchRequestDateTime { get; set; }
    }
}