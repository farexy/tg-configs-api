using System;
using System.Collections.Generic;

namespace TG.Configs.Api.Models.Response
{
    public class ConfigManagementResponse
    {
        public string Id { get; set; } = default!;
        
        public string Secret { get; set; } = default!;
        
        public string? Content { get; set; }

        public string Format { get; set; } = default!;
        
        public bool HasSecrets { get; set; }

        public string CreatedBy { get; set; } = default!;
        
        public DateTime CreatedAt{ get; set; }
        
        public string UpdatedBy { get; set; } = default!;
        
        public DateTime UpdatedAt{ get; set; }
        
        public IReadOnlyList<CallbackResponse>? Callbacks { get; set; }
        
        public IReadOnlyCollection<ConfigVariableResponse>? Variables { get; set; }
    }
}