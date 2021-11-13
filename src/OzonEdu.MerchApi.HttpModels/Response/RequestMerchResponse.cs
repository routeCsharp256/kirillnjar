using System;

namespace OzonEdu.MerchApi.HttpModels.Response
{
    public class RequestMerchResponse
    {
        public bool IsSuccess { get; set; }
        public StatusType StatusType { get; set; }
    }
    
    /// <summary>
    /// Enum of request status
    /// </summary>
    public enum StatusType
    {
        Reserved = 0,
        AlreadyGiven = 1,
        OutOfStock = 2
    }
}