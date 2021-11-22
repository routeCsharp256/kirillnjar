namespace OzonEdu.MerchApi.HttpModels.Request
{
    public class RequestMerchPostViewModel
    {
        public string EmployeeEmail { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string MiddleName { get; set; }
        public int MerchPackId { get; set; }
    }
}