using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sher.Application.Access.GetUser;
using Sher.Application.Access.RegisterUser;

namespace Sher.Api.Controllers.Access
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
            var user = await _mediator.Send(new GetUserQuery(UserId));
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequestModel model)
        {
            await _mediator.Send(new RegisterUserCommand(
                model.UserId,
                model.EmailAddress, 
                model.Password,
                model.InvitationCode));

            return Ok();
        }
    }
}