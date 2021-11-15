using System;

namespace OzonEdu.MerchApi.Domain.Exceptions.MerchRequestAggregate
{
    public class MerchRequestStatusException : Exception
    {
        public MerchRequestStatusException(string message) : base(message)
        {
            
        }
        
        public MerchRequestStatusException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}