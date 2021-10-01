using System;

namespace TG.Configs.Api.Entities
{
    public class Callback
    {
        public Guid Id { get; set; }

        public string ConfigId { get; set; } = default!;
        
        public string? Url { get; set; }
    }
}