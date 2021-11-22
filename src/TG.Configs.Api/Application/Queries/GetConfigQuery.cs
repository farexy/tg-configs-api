using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TG.Configs.Api.Db;
using TG.Configs.Api.Errors;
using TG.Configs.Api.Helpers;
using TG.Configs.Api.Models.Response;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Queries
{
    public record GetConfigQuery(string Id) : IRequest<OperationResult<ConfigResponse>>;
    
    public class GetConfigQueryHandler : IRequestHandler<GetConfigQuery, OperationResult<ConfigResponse>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetConfigQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<ConfigResponse>> Handle(GetConfigQuery request, CancellationToken cancellationToken)
        {
            var config = await _dbContext.Configs
                .Include(c => c.Variables)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (config is null)
            {
                return AppErrors.NotFound;
            }

            return new ConfigResponse
            {
                Id = config.Id,
                Content = config.GetContentWithVariables(),
                UpdatedAt = config.UpdatedAt
            };
        }
    }
}