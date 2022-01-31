using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TG.Configs.Api.Db;
using TG.Configs.Api.Errors;
using TG.Configs.Api.Models.Response;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Queries
{
    public record GetConfigManagementQuery(string Id) : IRequest<OperationResult<ConfigManagementResponse>>;
    
    public class GetConfigManagementQueryHandler : IRequestHandler<GetConfigManagementQuery, OperationResult<ConfigManagementResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetConfigManagementQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OperationResult<ConfigManagementResponse>> Handle(GetConfigManagementQuery request, CancellationToken cancellationToken)
        {
            var config = await _dbContext.Configs
                .Include(c => c.Callbacks)
                .Include(c => c.Variables)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (config is null)
            {
                return AppErrors.NotFound;
            }

            return _mapper.Map<ConfigManagementResponse>(config);
        }
    }
}