using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TG.Configs.Api.Db;
using TG.Configs.Api.Entities;
using TG.Configs.Api.Services;
using TG.Core.App.OperationResults;
using TG.Core.App.Services;

namespace TG.Configs.Api.Application.Commands
{
    public record SaveConfigVariableCommand(string ConfigId, string Sign, string Key, string? Value) : IRequest<OperationResult>;
    
    public class SaveConfigVariableCommandHandler : IRequestHandler<SaveConfigVariableCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfigsCache _configsCache;

        public SaveConfigVariableCommandHandler(ApplicationDbContext dbContext, IConfigsCache configsCache)
        {
            _dbContext = dbContext;
            _configsCache = configsCache;
        }

        public async Task<OperationResult> Handle(SaveConfigVariableCommand request, CancellationToken cancellationToken)
        {
            var secret = await _dbContext.Configs
                .Where(c => c.Id == request.ConfigId)
                .Select(c => c.Secret)
                .FirstOrDefaultAsync(cancellationToken);
            
            var calculatedSign = Sha256Helper.GetSha256Hash(request.Key + request.Value + secret);

            if (calculatedSign != request.Sign)
            {
                throw new UnauthorizedAccessException();
            }
            
            var variable = await _dbContext.ConfigVariables
                .FirstOrDefaultAsync(v => v.ConfigId == request.ConfigId && v.Key == request.Key, cancellationToken);

            if (variable is null)
            {
                variable = new ConfigVariable
                {
                    ConfigId = request.ConfigId,
                    Key = request.Key,
                    Value = request.Value,
                };
                await _dbContext.AddAsync(variable, cancellationToken);
            }
            else
            {
                variable.Value = request.Value;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            _configsCache.Reset(request.ConfigId);

            return OperationResult.Success();
        }
    }
}