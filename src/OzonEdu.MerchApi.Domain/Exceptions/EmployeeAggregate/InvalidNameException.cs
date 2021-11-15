using System;

namespace OzonEdu.MerchApi.Domain.Exceptions.EmployeeAggregate
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message) : base(message)
        {
            
        }
        
        public InvalidNameException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}