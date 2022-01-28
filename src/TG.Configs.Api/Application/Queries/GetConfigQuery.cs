using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TG.Configs.Api.Errors;
using TG.Configs.Api.Models.Response;
using TG.Configs.Api.Services;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Queries
{
    public record GetConfigQuery(string Id) : IRequest<OperationResult<ConfigResponse>>;
    
    public class GetConfigQueryHandler : IRequestHandler<GetConfigQuery, OperationResult<ConfigResponse>>
    {
        private readonly IConfigsProvider _configsProvider;
        private readonly IMapper _mapper;

        public GetConfigQueryHandler(IConfigsProvider configsProvider, IMapper mapper)
        {
            _configsProvider = configsProvider;
            _mapper = mapper;
        }

        public async Task<OperationResult<ConfigResponse>> Handle(GetConfigQuery request, CancellationToken cancellationToken)
        {
            var config = await _configsProvider.GetAsync(request.Id, cancellationToken);
            if (config is null)
            {
                return AppErrors.NotFound;
            }

            return _mapper.Map<ConfigResponse>(config);
        }
    }
}