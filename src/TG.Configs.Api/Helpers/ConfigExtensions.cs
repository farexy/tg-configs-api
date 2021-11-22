using System.Linq;

namespace TG.Configs.Api.Helpers
{
    public static class ConfigExtensions
    {
        public static string? GetContentWithVariables(this Entities.Config config)
        {
            if (config.Content is null || config.Variables is null || config.Variables.Count == 0)
            {
                return config.Content;
            }

            return config.Variables.Aggregate(config.Content, (current, variable) =>
                current.Replace($"##{variable.Key}##", variable.Value));
        }
    }
}