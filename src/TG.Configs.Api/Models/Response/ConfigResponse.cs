using System;
using System.Collections.Generic;
using System.Text.Json;
using TG.Configs.Api.Entities;

namespace TG.Configs.Api.Models.Response
{
    public class ConfigResponse
    {
        public string Id { get; set; } = default!;
        
        public string Secret { get; set; } = default!;
        
        public string? Content { get; set; }

        public string CreatedBy { get; set; } = default!;
        
        public DateTime CreatedAt{ get; set; }
        
        public string UpdatedBy { get; set; } = default!;
        
        public DateTime UpdatedAt{ get; set; }
        
        public IReadOnlyList<Callback>? Callbacks { get; set; }
    }
}