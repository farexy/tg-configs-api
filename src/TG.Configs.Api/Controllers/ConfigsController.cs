using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TG.Configs.Api.Application.Queries;
using TG.Configs.Api.Config;
using TG.Configs.Api.Errors;
using TG.Core.App.Constants;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Controllers
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route(ServiceConst.RoutePrefix)]
    [Authorize]
    public class ConfigsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object?>> GetContent([FromRoute] string id, [FromQuery, Required] string secret)
        {
            var result = await _mediator.Send(new GetClientConfigContentQuery(id, secret));
            return result.ToActionResult()
                .NotFound(AppErrors.NotFound)
                .Ok();
        }
    }
}