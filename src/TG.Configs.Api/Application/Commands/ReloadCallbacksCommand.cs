using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TG.Configs.Api.Db;
using TG.Configs.Api.Services;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Commands
{
    public record ReloadCallbacksCommand(string ConfigId) : IRequest<OperationResult>;
    
    public class ReloadCallbacksCommandHandler : IRequestHandler<ReloadCallbacksCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICallbacksClient _callbacksClient;

        public ReloadCallbacksCommandHandler(ApplicationDbContext dbContext, ICallbacksClient callbacksClient)
        {
            _dbContext = dbContext;
            _callbacksClient = callbacksClient;
        }

        public async Task<OperationResult> Handle(ReloadCallbacksCommand request, CancellationToken cancellationToken)
        {
            var config = await _dbContext.Configs
                .Include(c => c.Callbacks)
                .FirstOrDefaultAsync(c => c.Id == request.ConfigId, cancellationToken);
            if (config.Callbacks is null)
            {
                return OperationResult.Success();
            }

            var results = await Task.WhenAll(config.Callbacks.Select(callback =>
                _callbacksClient.ReloadCallbackAsync(callback, cancellationToken)));
            var fail = results.FirstOrDefault(r => r.HasError);
            return fail ?? OperationResult.Success();
        }
    }
}