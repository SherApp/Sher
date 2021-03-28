using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sher.Application.Access.GetUser;

namespace Sher.Api.Controllers.Access
{
    [Authorize]
    [Route("[controller]")]
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
    }
}