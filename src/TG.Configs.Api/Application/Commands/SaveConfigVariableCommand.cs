using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TG.Configs.Api.Db;
using TG.Configs.Api.Entities;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Commands
{
    public record SaveConfigVariableCommand(string ConfigId, string Secret, string Key, string? Value) : IRequest<OperationResult>;
    
    public class SaveConfigVariableCommandHandler : IRequestHandler<SaveConfigVariableCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveConfigVariableCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveConfigVariableCommand request, CancellationToken cancellationToken)
        {
            if (!await _dbContext.Configs.AnyAsync(c => c.Id == request.ConfigId && c.Secret == request.Secret, cancellationToken))
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
            
            return OperationResult.Success();
        }
    }
}