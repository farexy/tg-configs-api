using System;
using System.Collections.Generic;
using System.Text.Json;

namespace TG.Configs.Api.Entities
{
    public class Config
    {
        public string Id { get; set; } = default!;
        
        public string Secret { get; set; } = default!;
        
        public JsonDocument? Content { get; set; }
        
        public Guid CreatedBy { get; set; }
        
        public DateTime CreatedAt{ get; set; }
        
        public Guid UpdatedBy { get; set; }
        
        public DateTime UpdatedAt{ get; set; }
        
        public IReadOnlyList<Callback>? Callbacks { get; set; }
    }
}