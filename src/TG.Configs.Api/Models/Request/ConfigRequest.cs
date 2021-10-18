using TG.Configs.Api.Entities;

namespace TG.Configs.Api.Models.Request
{
    public class ConfigRequest
    {
        public string Id { get; set; } = default!;
        
        public string? Content { get; set; }
        
        public ConfigFormat Format { get; set; }
    }
}