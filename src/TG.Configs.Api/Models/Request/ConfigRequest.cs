namespace TG.Configs.Api.Models.Request
{
    public class ConfigRequest
    {
        public string Id { get; set; } = default!;
        
        public string? Content { get; set; }
    }
}