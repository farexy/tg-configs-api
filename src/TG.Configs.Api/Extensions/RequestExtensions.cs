using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using TG.Configs.Api.Config;

namespace TG.Configs.Api.Extensions
{
    public static class RequestExtensions
    {
        public static string GetConfigSecret(this HttpRequest request)
        {
            if (request.Headers.TryGetValue(ConfigHeaders.SecretKey, out var secrets))
            {
                return secrets.First();
            }

            throw new UnauthorizedAccessException();
        }
    }
}