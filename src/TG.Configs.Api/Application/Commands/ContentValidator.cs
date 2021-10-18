using System;
using System.Text.Json;

namespace TG.Configs.Api.Application.Commands
{
    public static class ContentValidator
    {
        public static bool IsValid(string? content, out string? optimizedJson)
        {
            optimizedJson = null!;
            if (content is null)
            {
                return true;
            }
            try
            {
                optimizedJson = JsonDocument.Parse(content).ToString();
            }
            catch (JsonException)
            {
                return false;
            }

            return true;
        }
    }
}