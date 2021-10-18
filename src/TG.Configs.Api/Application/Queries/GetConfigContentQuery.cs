using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TG.Configs.Api.Db;
using TG.Configs.Api.Errors;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Queries
{
    public record GetConfigContentQuery(string Id) : IRequest<OperationResult<string?>>;
    
    public class GetConfigContentQueryHandler : IRequestHandler<GetConfigContentQuery, OperationResult<string?>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetConfigContentQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<string?>> Handle(GetConfigContentQuery request, CancellationToken cancellationToken)
        {
            var config = await _dbContext.Configs.FindAsync(request.Id);
            if (config is null)
            {
                return AppErrors.NotFound;
            }

            return config.Content;
        }
    }
}