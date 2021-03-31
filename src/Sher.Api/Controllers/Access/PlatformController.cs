using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sher.Application.Access.GetPlatformSettings;
using Sher.Application.Access.SetPlatformSettings;

namespace Sher.Api.Controllers.Access
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class PlatformController : ApiController
    {
        private readonly IMediator _mediator;

        public PlatformController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("settings")]
        public async Task<IActionResult> GetPlatformSettingsAsync()
        {
            var settings = await _mediator.Send(new GetPlatformSettingsQuery());
            return Ok(settings);
        }

        [HttpPut]
        [Route("settings")]
        public async Task<IActionResult> SetPlatformSettingsAsync([FromBody] PlatformSettingsDto settings)
        {
            await _mediator.Send(new SetPlatformSettingsCommand(settings));
            return Ok();
        }
    }
}