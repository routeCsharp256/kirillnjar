using System;

namespace OzonEdu.MerchApi.Domain.Exceptions.MerchPackAggregate
{
    public class MerchPackMultipleFoundException: Exception
    {
        public MerchPackMultipleFoundException(string message) : base(message)
        {
            
        }
        
        public MerchPackMultipleFoundException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}