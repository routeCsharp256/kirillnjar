using System;

namespace OzonEdu.MerchApi.Domain.Exceptions.MerchItemAggregate
{
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException(string message) : base(message)
        {
            
        }
        
        public InvalidQuantityException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}