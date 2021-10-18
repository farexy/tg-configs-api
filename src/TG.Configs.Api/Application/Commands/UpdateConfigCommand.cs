using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TG.Configs.Api.Db;
using TG.Configs.Api.Entities;
using TG.Configs.Api.Errors;
using TG.Configs.Api.Models.Response;
using TG.Core.App.OperationResults;
using TG.Core.App.Services;

namespace TG.Configs.Api.Application.Commands
{
    public record UpdateConfigCommand(string Id, string? Content, ConfigFormat Format, string UserEmail) : IRequest<OperationResult<ConfigManagementResponse>>;
    
    public class UpdateConfigCommandHandler : IRequestHandler<UpdateConfigCommand, OperationResult<ConfigManagementResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateConfigCommandHandler(ApplicationDbContext dbContext, IMapper mapper,
            IDateTimeProvider dateTimeProvider)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<OperationResult<ConfigManagementResponse>> Handle(UpdateConfigCommand request, CancellationToken cancellationToken)
        {
            string? optimizedContent = request.Content;
            if (request.Format is ConfigFormat.Json && !ContentValidator.IsValid(request.Content, out optimizedContent))
            {
                return AppErrors.InvalidContent;
            }
            var config = await _dbContext.Configs.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            config.Content = optimizedContent;
            config.UpdatedBy = request.UserEmail;
            config.UpdatedAt = _dateTimeProvider.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ConfigManagementResponse>(config);
        }
    }
}