using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TG.Configs.Api.Errors;
using TG.Configs.Api.Services;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Queries
{
    public record GetConfigContentQuery(string Id) : IRequest<OperationResult<string?>>;
    
    public class GetConfigContentQueryHandler : IRequestHandler<GetConfigContentQuery, OperationResult<string?>>
    {
        private readonly IConfigsProvider _configsProvider;

        public GetConfigContentQueryHandler(IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
        }

        public async Task<OperationResult<string?>> Handle(GetConfigContentQuery request, CancellationToken cancellationToken)
        {
            var config = await _configsProvider.GetAsync(request.Id, cancellationToken);
            if (config is null)
            {
                return AppErrors.NotFound;
            }

            return config.Content;
        }
    }
}