using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TG.Configs.Api.Db;
using TG.Configs.Api.Entities;
using TG.Configs.Api.ServiceClients;
using TG.Configs.Api.Services;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Commands
{
    public record ReloadCallbacksCommand(string ConfigId) : IRequest<OperationResult>;
    
    public class ReloadCallbacksCommandHandler : IRequestHandler<ReloadCallbacksCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICallbacksClient _callbacksClient;
        private readonly IAppsManagerClient _appsManagerClient;

        public ReloadCallbacksCommandHandler(ApplicationDbContext dbContext, ICallbacksClient callbacksClient, IAppsManagerClient appsManagerClient)
        {
            _dbContext = dbContext;
            _callbacksClient = callbacksClient;
            _appsManagerClient = appsManagerClient;
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

            var results = await Task.WhenAll(config.Callbacks.Select(async c =>
            {
                var urls = await GetUrlsAsync(c, cancellationToken);
                return await Task.WhenAll(urls.Select(url => _callbacksClient.ReloadCallbackAsync(url, config.Id, cancellationToken)));
            }));
            var fail = results.SelectMany(r => r).FirstOrDefault(r => r.HasError);
            return fail ?? OperationResult.Success();
        }

        private async Task<List<string>> GetUrlsAsync(Callback callback, CancellationToken cancellationToken)
        {
            var urls = new List<string>();
            if (!string.IsNullOrEmpty(callback.Url))
            {
                urls.Add(callback.Url);
            }

            if (!string.IsNullOrEmpty(callback.TgApp))
            {
                var result = await _appsManagerClient.GetEndpointsAsync(callback.TgApp, cancellationToken);
                urls.AddRange(result.Result!.Endpoints!.Select(e => e.Ip)
                    .Select(ip => BuildConfigReloadUrlByTgApp(callback.TgApp, ip)));
            }

            return urls;
        }

        private static string BuildConfigReloadUrlByTgApp(string tgApp, string ip)
        {
            var routePrefix = tgApp.Replace("-api", string.Empty);
            return $"http://{ip}:80/internal/{routePrefix}/configs/";
        }
    }
}