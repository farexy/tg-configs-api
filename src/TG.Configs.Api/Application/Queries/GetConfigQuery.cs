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
    public record GetConfigQuery(string Id) : IRequest<OperationResult<ConfigResponse>>;
    
    public class GetConfigQueryHandler : IRequestHandler<GetConfigQuery, OperationResult<ConfigResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetConfigQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OperationResult<ConfigResponse>> Handle(GetConfigQuery request, CancellationToken cancellationToken)
        {
            var config = await _dbContext.Configs
                .Include(c => c.Callbacks)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (config is null)
            {
                return AppErrors.NotFound;
            }

            return _mapper.Map<ConfigResponse>(config);
        }
    }
}