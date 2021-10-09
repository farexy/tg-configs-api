using System.Threading;
using System.Threading.Tasks;
using TG.Configs.Api.Entities;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Services
{
    public interface ICallbacksClient
    {
        Task<OperationResult> ReloadCallbackAsync(Callback callback, string configSecret, CancellationToken cancellationToken);
    }
}