using System;
using TG.Configs.Api.Entities;

namespace TG.Configs.Api.Models.Response
{
    public class ConfigResponse
    {
        public string Id { get; set; } = default!;
        
        public string? Content { get; set; }
        
        public ConfigFormat Format { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}