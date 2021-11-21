using System;

namespace OzonEdu.MerchApi.Domain.Exceptions
{
    public class RequiredEventPropertyIsNullException : ArgumentNullException
    {
        
        public RequiredEventPropertyIsNullException(string message) : base(message)
        {
            
        }
        
        public RequiredEventPropertyIsNullException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
        
        public RequiredEventPropertyIsNullException(string paramName = null, string message = null) : base(paramName, message)
        {
            
        }
    }
}