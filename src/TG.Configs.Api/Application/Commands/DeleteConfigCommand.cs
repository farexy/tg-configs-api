using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TG.Configs.Api.Db;
using TG.Configs.Api.Errors;
using TG.Configs.Api.Services;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Commands
{
    public record DeleteConfigCommand(string Id, string UserEmail) : IRequest<OperationResult>;
    
    public class DeleteConfigCommandHandler : IRequestHandler<DeleteConfigCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfigsCache _configsCache;

        public DeleteConfigCommandHandler(ApplicationDbContext dbContext, IConfigsCache configsCache)
        {
            _dbContext = dbContext;
            _configsCache = configsCache;
        }

        public async Task<OperationResult> Handle(DeleteConfigCommand request, CancellationToken cancellationToken)
        {
            var config = await _dbContext.Configs.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (config is null)
            {
                return AppErrors.NotFound;
            }
            
            _dbContext.Remove(config);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _configsCache.ResetAsync(config.Id);

            return OperationResult.Success();
        }
    }
}