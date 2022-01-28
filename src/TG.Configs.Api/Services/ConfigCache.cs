using System.Collections.Concurrent;
using TG.Configs.Api.Models.Dto;

namespace TG.Configs.Api.Services
{
    public class ConfigsCache : IConfigsCache
    {
        private readonly ConcurrentDictionary<string, ConfigData> _cache = new();

        public ConfigData? Find(string configId)
        {
            return _cache.TryGetValue(configId, out var cnt) ? cnt : default;
        }

        public ConfigData? Set(string configId, ConfigData data)
        {
            return _cache.AddOrUpdate(configId, data, (_,_) => data);
        }

        public void Reset(string configId)
        {
            _cache.TryRemove(configId, out _);
        }

        public void Reset()
        {
            _cache.Clear();
        }
    }
}