using System;

namespace OzonEdu.MerchApi.Domain.Exceptions.MerchPackAggregate
{
    public class InvalidSkuException : Exception
    {
        public InvalidSkuException(string message) : base(message)
        {
            
        }
        
        public InvalidSkuException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}