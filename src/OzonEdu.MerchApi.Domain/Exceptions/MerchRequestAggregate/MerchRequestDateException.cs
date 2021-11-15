using System;

namespace OzonEdu.MerchApi.Domain.Exceptions.MerchRequestAggregate
{
    public class MerchRequestDateException : Exception
    {
        public MerchRequestDateException(string message) : base(message)
        {
            
        }
        
        public MerchRequestDateException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}