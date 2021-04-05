using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sher.Application.Access.GetPlatformSettings;
using Sher.Application.Access.SetPlatformSettings;

namespace Sher.Api.Controllers.Access
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PlatformController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PlatformController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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

        [HttpGet]
        [AllowAnonymous]
        [Route("settings/registration")]
        public async Task<IActionResult> GetRegistrationSettings()
        {
            var platformSettings = await _mediator.Send(new GetPlatformSettingsQuery());
            var registrationSettings = _mapper.Map<RegistrationSettingsResponse>(platformSettings);

            return Ok(registrationSettings);
        }
    }
}