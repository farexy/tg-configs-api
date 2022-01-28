using System.Threading;
using System.Threading.Tasks;
using TG.Configs.Api.Models.Dto;

namespace TG.Configs.Api.Services;

public interface IConfigsProvider
{
    Task<ConfigData?> GetAsync(string configId, CancellationToken cancellationToken);
}