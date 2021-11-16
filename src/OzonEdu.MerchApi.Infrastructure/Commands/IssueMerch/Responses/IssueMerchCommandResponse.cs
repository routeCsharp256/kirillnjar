﻿using OzonEdu.MerchApi.Enums;

namespace OzonEdu.MerchApi.Infrastructure.Commands.IssueMerch.Responses
{
    public class IssueMerchCommandResponse
    {
        public bool IsSuccess { get; set; }
        public StatusType StatusType { get; set; }
    }
}