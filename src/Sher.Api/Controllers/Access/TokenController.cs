using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sher.Api.Common;
using Sher.Application.Access.AuthenticateUser;
using Sher.Application.Access.RefreshAuthToken;

namespace Sher.Api.Controllers.Access
{
    [Route("api/[controller]")]
    public class TokenController : ApiController
    {
        private const string JwtCookieName = "JwtToken";
        private const string RefreshCookieName = "RefreshToken";

        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("new")]
        public async Task<IActionResult> IssueTokenAsync(
            [FromBody] IssueTokenRequestModel model,
            [FromQuery] bool asCookie = true)
        {
            var result = await _mediator.Send(new AuthenticateUserCommand(model.EmailAddress, model.Password));

            if (result is null)
            {
                return Unauthorized(new ErrorModel("Wrong login details"));
            }

            if (!asCookie) return Ok(result);

            AppendAuthCookies(result.JwtToken, result.RefreshToken);
            return Ok();
        }

        [HttpPost]
        [Authorize("TokenRefresh")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequestModel model = null)
        {
            if (!Request.Cookies.TryGetValue(RefreshCookieName, out var refreshToken))
            {
                refreshToken = model?.RefreshToken;
            }

            if (refreshToken is null)
            {
                return Unauthorized();
            }

            var result = await _mediator.Send(new RefreshAuthTokenCommand(UserId, refreshToken));
            if (result is null)
            {
                return Unauthorized();
            }

            if (model?.RefreshToken is not null)
            {
                return Ok(result);
            }

            AppendAuthCookies(result.JwtToken, result.RefreshToken);
            
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAuthCookies()
        {
            Response.Cookies.Delete(JwtCookieName);
            Response.Cookies.Delete(RefreshCookieName);

            return Ok();
        }

        private void AppendAuthCookies(string jwtToken, string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append(JwtCookieName, jwtToken, cookieOptions);
            Response.Cookies.Append(RefreshCookieName, refreshToken, cookieOptions);
        }
    }
}