using System;

namespace OzonEdu.MerchApi.Domain.Exceptions.MerchPackAggregate
{
    public class MerchPackNotFoundException: Exception
    {
        public MerchPackNotFoundException(string message) : base(message)
        {
            
        }
        
        public MerchPackNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}