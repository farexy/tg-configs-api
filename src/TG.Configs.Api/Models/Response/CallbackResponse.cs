using System;

namespace TG.Configs.Api.Models.Response
{
    public class CallbackResponse
    {
        public Guid Id { get; set; }

        public string ConfigId { get; set; } = default!;
        
        public string? Url { get; set; }
        
        public string? TgApp { get; set; }
    }
}