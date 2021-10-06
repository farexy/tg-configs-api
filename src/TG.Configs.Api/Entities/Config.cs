using System;
using System.Collections.Generic;

namespace TG.Configs.Api.Entities
{
    public class Config
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