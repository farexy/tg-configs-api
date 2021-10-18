using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TG.Configs.Api.Application.Commands;
using TG.Configs.Api.Application.Queries;
using TG.Configs.Api.Config;
using TG.Configs.Api.Errors;
using TG.Configs.Api.Models.Request;
using TG.Configs.Api.Models.Response;
using TG.Core.App.Constants;
using TG.Core.App.Extensions;
using TG.Core.App.OperationResults;

namespace TG.Configs.Api.Controllers
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [Route(ServiceConst.RoutePrefix)]
    [Authorize(Roles = TgUserRoles.Admin)]
    public class ConfigurationManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigurationManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ConfigItemsResponse>> Get()
        {
            var result = await _mediator.Send(new GetConfigItemsQuery());
            return result.ToActionResult()
                .Ok();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ConfigManagementResponse>> Get([FromRoute] string id)
        {
            var result = await _mediator.Send(new GetConfigManagementQuery(id));
            return result.ToActionResult()
                .NotFound(AppErrors.NotFound)
                .Ok();
        }
        
        [HttpPost]
        public async Task<ActionResult<ConfigManagementResponse>> Create([FromBody] ConfigRequest request)
        {
            var result = await _mediator.Send(new CreateConfigCommand(request.Id, request.Content, request.Format, User.GetEmail()));
            return result.ToActionResult()
                .Created();
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<ConfigManagementResponse>> Update([FromRoute] string id, [FromBody] ConfigRequest request)
        {
            var result = await _mediator.Send(new UpdateConfigCommand(id, request.Content, request.Format, User.GetEmail()));
            return result.ToActionResult()
                .NotFound(AppErrors.NotFound)
                .Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            var result = await _mediator.Send(new DeleteConfigCommand(id, User.GetEmail()));
            return result.ToActionResult()
                .NotFound(AppErrors.NotFound)
                .NoContent();
        }
        
        [HttpPost("{id}/reload")]
        public async Task<ActionResult<OperationResult>> Reload([FromRoute] string id)
        {
            var result = await _mediator.Send(new ReloadCallbacksCommand(id));
            return result.ToActionResult()
                .NoContent();
        }
    }
}