using System.Collections.Concurrent;

namespace TG.Configs.Api.Services
{
    public class ConfigContentCache : IConfigContentCache
    {
        private readonly ConcurrentDictionary<string, string?> _cache = new();

        public string? Find(string configId, string secret)
        {
            return _cache.TryGetValue(BuildKey(configId, secret), out var cnt) ? cnt : default;
        }

        public string? Set(string configId, string secret, string? content)
        {
            return _cache.AddOrUpdate(BuildKey(configId, secret), content, (_,_) => content);
        }

        public void Reset()
        {
            _cache.Clear();
        }

        private static string BuildKey(string configId, string secret) => $"{configId}{secret}";
    }
}