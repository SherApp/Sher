using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sher.Application.Access.AuthenticateUser;
using Sher.Application.Access.RefreshAuthToken;

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
            var result = await _mediator.Send(new AuthenticateUserCommand(model.EmailAddress, model.Password));

            if (result is null)
            {
                return Unauthorized();
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };
            Response.Cookies.Append("JwtToken", result.JwtToken, cookieOptions);
            Response.Cookies.Append("RefreshToken", result.RefreshToken, cookieOptions);

            return Ok();
        }

        [HttpGet]
        [Authorize("TokenRefresh")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
            {
                return Unauthorized();
            }

            var result = await _mediator.Send(new RefreshAuthTokenCommand(UserId, refreshToken));
            if (result is null)
            {
                return Unauthorized();
            }

            AppendAuthCookies(result.JwtToken, result.RefreshToken);
            
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

            Response.Cookies.Append("JwtToken", jwtToken, cookieOptions);
            Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);
        }
    }
}