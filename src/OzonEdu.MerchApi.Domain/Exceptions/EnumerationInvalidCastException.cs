using System;

namespace OzonEdu.MerchApi.Domain.Exceptions
{
    public class EnumerationInvalidCastException  : Exception
    {
        public EnumerationInvalidCastException(string message) : base(message)
        {
            
        }
        
        public EnumerationInvalidCastException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}