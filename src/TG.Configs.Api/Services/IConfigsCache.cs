using TG.Configs.Api.Models.Dto;

namespace TG.Configs.Api.Services
{
    public interface IConfigsCache
    {
        ConfigData? Find(string configId);
        ConfigData? Set(string configId, ConfigData data);
        void Reset(string configId);
        void Reset();
    }
}