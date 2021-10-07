using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TG.Configs.Api.Db;
using TG.Configs.Api.Errors;
using TG.Configs.Api.Models.Response;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Queries
{
    public record GetConfigQuery(string Id, string Secret) : IRequest<OperationResult<ConfigResponse>>;
    
    public class GetConfigQueryHandler : IRequestHandler<GetConfigQuery, OperationResult<ConfigResponse>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetConfigQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<ConfigResponse>> Handle(GetConfigQuery request, CancellationToken cancellationToken)
        {
            var config = await _dbContext.Configs.FindAsync(request.Id);
            if (config is null)
            {
                return AppErrors.NotFound;
            }

            if (!config.Secret.SequenceEqual(request.Secret))
            {
                return AppErrors.InvalidSecret;
            }

            return new ConfigResponse
            {
                Id = config.Id,
                Content = config.Content,
                UpdatedAt = config.UpdatedAt
            };
        }
    }
}