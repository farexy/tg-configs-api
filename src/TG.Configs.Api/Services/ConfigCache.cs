using System;
using System.Threading.Tasks;
using StackExchange.Redis;
using TG.Configs.Api.Models.Dto;
using TG.Core.App.Json;

namespace TG.Configs.Api.Services
{
    public class ConfigsCache : IConfigsCache
    {
        private static readonly TimeSpan CacheProlong = TimeSpan.FromMinutes(10);
        private const string ConfigsKey = "configs";
        private readonly IDatabase _redis;

        public ConfigsCache(IDatabase redis)
        {
            _redis = redis;
        }

        public async Task<ConfigData?> FindAsync(string configId)
        {
            var data = await _redis.HashGetAsync(ConfigsKey, configId);
            return data.HasValue ? TgJsonSerializer.Deserialize<ConfigData>(data) : null;
        }

        public async Task SetAsync(string configId, ConfigData data)
        {
            await _redis.HashSetAsync(ConfigsKey, configId, TgJsonSerializer.Serialize(data));
            await _redis.KeyExpireAsync(ConfigsKey, CacheProlong);
        }

        public Task ResetAsync(string configId)
        {
            return _redis.HashDeleteAsync(ConfigsKey, configId);
        }
    }
}