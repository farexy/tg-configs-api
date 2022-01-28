using System;
using TG.Configs.Api.Entities;

namespace TG.Configs.Api.Models.Dto;

public class ConfigData
{
    public string Id { get; set; } = default!;
    
    public DateTime UpdatedAt { get; set; }

    public string? Content { get; set; } = default!;

    public string Secret { get; set; } = default!;
    
    public ConfigFormat Format { get; set; }
}