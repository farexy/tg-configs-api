namespace TG.Configs.Api.Models.Response;

public class ConfigVariableResponse
{
    public string ConfigId { get; set; } = default!;

    public string Key { get; set; } = default!;
        
    public string? Value { get; set; }
}