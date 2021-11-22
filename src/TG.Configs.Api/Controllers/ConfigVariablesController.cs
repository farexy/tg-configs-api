using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TG.Configs.Api.Application.Commands;
using TG.Configs.Api.Config;
using TG.Configs.Api.Models.Request;
using TG.Core.App.Constants;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Controllers
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route(ServiceConst.RoutePrefix)]
    public class ConfigVariablesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigVariablesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{configId}")]
        public async Task<ActionResult> SaveVariable([FromRoute] string configId, [FromQuery, Required] string secret, [FromBody] ConfigVariableRequest request)
        {
            var result = await _mediator.Send(new SaveConfigVariableCommand(configId, secret, request.Key, request.Value));
            return result.ToActionResult()
                .NoContent();
        }
    }
}