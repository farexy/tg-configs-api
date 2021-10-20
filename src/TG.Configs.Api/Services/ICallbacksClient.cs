using System.Threading;
using System.Threading.Tasks;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Services
{
    public interface ICallbacksClient
    {
        Task<OperationResult> ReloadCallbackAsync(string url, string configId, CancellationToken cancellationToken);
    }
}