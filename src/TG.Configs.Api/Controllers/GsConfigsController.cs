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
    [Route(ServiceConst.RoutePrefix)]
    public class GsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConfigResponse>> Get([FromRoute] string id)
        {
            var result = await _mediator.Send(new GetConfigQuery(id));
            return result.ToActionResult()
                .NotFound(AppErrors.NotFound)
                .Ok();
        }
    }
}