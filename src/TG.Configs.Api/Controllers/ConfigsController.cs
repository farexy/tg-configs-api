using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TG.Configs.Api.Application.Queries;
using TG.Configs.Api.Config;
using TG.Configs.Api.Errors;
using TG.Configs.Api.Models.Response;
using TG.Core.App.Constants;
using TG.Core.App.InternalCalls;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Controllers
{
    [GsApi]
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route(ServiceConst.BaseRoutePrefix)]
    public class ConfigsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConfigResponse>> Get([FromRoute] string id)
        {
            var result = await _mediator.Send(new GetConfigQuery(id));
            return result.ToActionResult()
                .BadRequest(AppErrors.InvalidSecret)
                .NotFound(AppErrors.NotFound)
                .Ok();
        }

        [HttpGet("{id}/content")]
        public async Task<ActionResult<string?>> GetContent([FromRoute] string id)
        {
            var result = await _mediator.Send(new GetConfigContentQuery(id));
            return result.ToActionResult()
                .BadRequest(AppErrors.InvalidSecret)
                .NotFound(AppErrors.NotFound)
                .Ok();
        }
    }
}