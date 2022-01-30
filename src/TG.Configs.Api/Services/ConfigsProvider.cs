using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TG.Configs.Api.Db;
using TG.Configs.Api.Helpers;
using TG.Configs.Api.Models.Dto;

namespace TG.Configs.Api.Services;

public class ConfigsProvider : IConfigsProvider
{
    private readonly IConfigsCache _cache;
    private readonly ApplicationDbContext _dbContext;
    private readonly ISecretsService _secretsService;

    public ConfigsProvider(IConfigsCache cache, ApplicationDbContext dbContext, ISecretsService secretsService)
    {
        _cache = cache;
        _dbContext = dbContext;
        _secretsService = secretsService;
    }

    public async Task<ConfigData?> GetAsync(string configId, CancellationToken cancellationToken)
    {
        var cachedData = await _cache.FindAsync(configId);
        if (cachedData is not null)
        {
            return cachedData;
        }

        var config = await _dbContext.Configs
            .Include(c => c.Variables)
            .FirstOrDefaultAsync(c => c.Id == configId, cancellationToken);

        if (config is null)
        {
            return null;
        }
        var content = config.GetContentWithVariables();
        if (config.HasSecrets && content is not null)
        {
            content = await _secretsService.EnrichContentWithSecrets(content, cancellationToken);
        }

        cachedData = new ConfigData
        {
            UpdatedAt = config.UpdatedAt,
            Content = content,
            Secret = config.Secret,
        };
        await _cache.SetAsync(configId, cachedData);

        return cachedData;
    }
}