using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TG.Configs.Api.Models.Response;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Queries
{
    public record GetConfigQuery(string Id) : IRequest<OperationResult<ConfigResponse>>;
    
    public class GetConfigQueryHandler : IRequestHandler<GetConfigQuery, OperationResult<ConfigResponse>>
    {
        public Task<OperationResult<ConfigResponse>> Handle(GetConfigQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}