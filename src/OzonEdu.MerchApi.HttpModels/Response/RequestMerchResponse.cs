using OzonEdu.MerchApi.Enums;

namespace OzonEdu.MerchApi.HttpModels.Response
{
    public class RequestMerchResponse
    {
        public bool IsSuccess { get; set; }
        public StatusType StatusType { get; set; }
    }
}