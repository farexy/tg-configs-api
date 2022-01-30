using System.Threading.Tasks;
using TG.Configs.Api.Models.Dto;

namespace TG.Configs.Api.Services
{
    public interface IConfigsCache
    {
        Task<ConfigData?> FindAsync(string configId);
        Task SetAsync(string configId, ConfigData data);
        Task ResetAsync(string configId);
    }
}