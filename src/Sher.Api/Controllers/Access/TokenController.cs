using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sher.Application.Access.AuthenticateUser;

namespace Sher.Api.Controllers.Access
{
    [Route("[controller]")]
    public class TokenController : ApiController
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> IssueTokenAsync([FromBody] IssueTokenRequestModel model)
        {
            var token = await _mediator.Send(new AuthenticateUserCommand(model.EmailAddress, model.Password));

            if (token is null)
            {
                return Unauthorized();
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };
            Response.Cookies.Append("JwtToken", token, cookieOptions);

            return Ok();
        }

        [HttpGet]
        [Authorize("TokenRefresh")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            return Ok();
        }
    }
}