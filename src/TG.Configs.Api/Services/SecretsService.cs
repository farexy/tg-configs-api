using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;

namespace TG.Configs.Api.Services;

public class SecretsService : ISecretsService
{
    private readonly Regex _secretPattern = new Regex(@"__(.*?)__");
    private readonly SecretClient _client;

    public SecretsService(SecretClient client)
    {
        _client = client;
    }

    public async Task<string> EnrichContentWithSecrets(string content, CancellationToken cancellationToken)
    {
        var matches = _secretPattern.Matches(content);
        var secretValues = await Task.WhenAll(matches.Select(key =>
            _client.GetSecretAsync(key.Value.Trim('_'), cancellationToken: cancellationToken)));
        return secretValues.Aggregate(content, (current, secretValue) =>
            current.Replace($"__{secretValue.Value.Name}__", secretValue.Value.Value));
    }
}