namespace TG.Configs.Api.Models.Request
{
    public class ConfigRequest
    {
        public string Id { get; set; } = default!;
        
        public string? Content { get; set; }

        public string Format { get; set; } = default!;
        
        public bool HasSecrets { get; set; }
    }
}