using System;

namespace OzonEdu.MerchApi.Domain.Exceptions.EmployeeAggregate
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message)
        {
            
        }
        
        public InvalidEmailException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}