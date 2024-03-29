﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TG.Configs.Api.Db;
using TG.Configs.Api.Models.Response;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Application.Queries
{
    public record GetConfigItemsQuery : IRequest<OperationResult<ConfigItemsResponse>>;
    
    public class GetConfigItemsQueryHandler : IRequestHandler<GetConfigItemsQuery, OperationResult<ConfigItemsResponse>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetConfigItemsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OperationResult<ConfigItemsResponse>> Handle(GetConfigItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _dbContext.Configs.ToListAsync(cancellationToken);
            return new ConfigItemsResponse(_mapper.Map<IEnumerable<ConfigItemResponse>>(items));
        }
    }
}