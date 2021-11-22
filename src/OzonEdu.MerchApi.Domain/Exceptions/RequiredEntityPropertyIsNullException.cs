using System;

namespace OzonEdu.MerchApi.Domain.Exceptions
{
    public class RequiredEntityPropertyIsNullException : ArgumentNullException
    {
        
        public RequiredEntityPropertyIsNullException(string message) : base(message)
        {
            
        }
        
        public RequiredEntityPropertyIsNullException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
        
        public RequiredEntityPropertyIsNullException(string paramName = null, string message = null) : base(paramName, message)
        {
            
        }
    }
}