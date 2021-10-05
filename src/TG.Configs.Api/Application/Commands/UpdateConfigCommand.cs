using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TG.Configs.Api.Db;
using TG.Configs.Api.Models.Response;
using TG.Core.App.OperationResults;
using TG.Core.App.Services;

namespace TG.Configs.Api.Application.Commands
{
    public record UpdateConfigCommand(string Id, object? Content, string UserEmail) : IRequest<OperationResult<ConfigResponse>>;
    
    public class UpdateConfigCommandHandler : IRequestHandler<UpdateConfigCommand, OperationResult<ConfigResponse>>
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

        public async Task<OperationResult<ConfigResponse>> Handle(UpdateConfigCommand request, CancellationToken cancellationToken)
        {
            var config = await _dbContext.Configs.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            config.Content = request.Content;
            config.UpdatedBy = request.UserEmail;
            config.UpdatedAt = _dateTimeProvider.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ConfigResponse>(config);
        }
    }
}