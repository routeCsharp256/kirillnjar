using OzonEdu.MerchApi.Enums;

namespace OzonEdu.MerchApi.Services.Commands.IssueMerch.Responses
{
    public class IssueMerchCommandResponse
    {
        public bool IsSuccess { get; init; }
        public StatusType StatusType { get; init; }
    }
}