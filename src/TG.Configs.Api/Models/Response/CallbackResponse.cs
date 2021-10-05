using System;

namespace TG.Configs.Api.Models.Response
{
    public class CallbackResponse
    {
        public Guid Id { get; set; }
        
        public string? Url { get; set; }
    }
}