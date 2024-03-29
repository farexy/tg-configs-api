﻿using System.Threading;
using System.Threading.Tasks;
using RestEase;
using TG.Configs.Api.Models.Dto;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.ServiceClients
{
    public interface IAppsManagerClient
    {
        [Get("v1/apps/endpoints")]
        Task<OperationResult<AppEndpointAddressesDto>> GetEndpointsAsync([Query] string app, CancellationToken cancellationToken);
    }
}