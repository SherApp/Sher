using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sher.Application.Processing;
using Sher.Core.Access;
using Sher.SharedKernel.Options;

namespace Sher.Application.Access.AuthenticateUser
{
    public class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, string>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly JwtOptions _options;

        public AuthenticateUserCommandHandler(
            IAuthenticationService authenticationService,
            IOptions<JwtOptions> options)
        {
            _authenticationService = authenticationService;
            _options = options.Value;
        }

        public async Task<string> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var (emailAddress, password) = request;

            var authenticationResult = await _authenticationService.AuthenticateUserAsync(emailAddress, password);
            if (authenticationResult is null)
            {
                return null;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, authenticationResult.NameIdentifier)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}