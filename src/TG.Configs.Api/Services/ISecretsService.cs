using System.Threading;
using System.Threading.Tasks;

namespace TG.Configs.Api.Services;

public interface ISecretsService
{
    Task<string> EnrichContentWithSecrets(string content, CancellationToken cancellationToken);
}