using System;
using System.Text.Json;

namespace TG.Configs.Api.Application.Commands
{
    public static class ContentValidator
    {
        public static bool IsValid(string? content)
        {
            if (content is null)
            {
                return true;
            }
            try
            {
                JsonSerializer.Deserialize<object>(content);
            }
            catch (JsonException)
            {
                return false;
            }

            return true;
        }
    }
}