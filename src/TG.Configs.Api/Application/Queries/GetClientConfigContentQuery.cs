using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TG.Configs.Api.Db;
using TG.Configs.Api.Errors;
using TG.Configs.Api.Helpers;
using TG.Configs.Api.Services;
using TG.Core.App.Json;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Queries
{
    public record GetClientConfigContentQuery(string Id, string Secret) : IRequest<OperationResult<object?>>;
    
    public class GetClientConfigContentQueryHandler : IRequestHandler<GetClientConfigContentQuery, OperationResult<object?>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfigContentCache _contentCache;

        public GetClientConfigContentQueryHandler(ApplicationDbContext dbContext, IConfigContentCache contentCache)
        {
            _dbContext = dbContext;
            _contentCache = contentCache;
        }

        public async Task<OperationResult<object?>> Handle(GetClientConfigContentQuery request, CancellationToken cancellationToken)
        {
            var (configId, secret) = request;
            var content = _contentCache.Find(configId, secret);
            if (content is null)
            {
                var config = await _dbContext.Configs
                    .Include(c => c.Variables)
                    .FirstOrDefaultAsync(c => c.Id == configId && c.Secret == secret, cancellationToken);
                if (config is null)
                {
                    return AppErrors.NotFound;
                }

                content = config.GetContentWithVariables();
                _contentCache.Set(configId, secret, content);
            }

            return TgJsonSerializer.Deserialize<object?>(content);
        }
    }
}