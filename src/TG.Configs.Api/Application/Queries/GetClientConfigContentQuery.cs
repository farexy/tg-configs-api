using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TG.Configs.Api.Errors;
using TG.Configs.Api.Services;
using TG.Core.App.Json;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Queries
{
    public record GetClientConfigContentQuery(string Id, string Secret) : IRequest<OperationResult<object?>>;
    
    public class GetClientConfigContentQueryHandler : IRequestHandler<GetClientConfigContentQuery, OperationResult<object?>>
    {
        private readonly IConfigsProvider _provider;

        public GetClientConfigContentQueryHandler(IConfigsProvider provider)
        {
            _provider = provider;
        }

        public async Task<OperationResult<object?>> Handle(GetClientConfigContentQuery request, CancellationToken cancellationToken)
        {
            var (configId, secret) = request;
            var config = await _provider.GetAsync(configId, cancellationToken);
            if (config is null || config.Secret != secret)
            {
                return AppErrors.NotFound;
            }

            return TgJsonSerializer.Deserialize<object?>(config.Content);
        }
    }
}