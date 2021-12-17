namespace TG.Configs.Api.Models.Request
{
    public class ConfigVariableRequest
    {
        public string Key { get; set; } = default!;
        
        public string? Value { get; set; }
    }
}