namespace OzonEdu.MerchApi.Infrastructure.Repositories.Models
{
    public class Employee
    {
        public int Id { get; init; }
        
        public string FirstName { get; init; }
        
        public string LastName { get; init; }
        
        public string MiddleName { get; init; }
        
        public string Email { get; init; }
        
        public int StatusId { get; init; }
    }
}