namespace TG.Configs.Api.Entities
{
    public class ConfigVariable
    {
        public string ConfigId { get; set; } = default!;

        public string Key { get; set; } = default!;
        
        public string? Value { get; set; }
    }
}