using System.Threading;
using System.Threading.Tasks;
using Sher.Application.Processing;
using Sher.Core.Access;

namespace Sher.Application.Access.AuthenticateUser
{
    public class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, AuthenticationResult>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly JwtIssuer _jwtIssuer;

        public AuthenticateUserCommandHandler(
            IAuthenticationService authenticationService,
            JwtIssuer jwtIssuer)
        {
            _authenticationService = authenticationService;
            _jwtIssuer = jwtIssuer;
        }

        public async Task<AuthenticationResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var (emailAddress, password) = request;

            var authenticationResult = await _authenticationService.AuthenticateUserAsync(emailAddress, password);
            if (authenticationResult is null)
            {
                return null;
            }

            var token = _jwtIssuer.IssueToken(authenticationResult.NameIdentifier, authenticationResult.Role);
            return new AuthenticationResult
            {
                JwtToken = token,
                RefreshToken = authenticationResult.RefreshToken
            };
        }
    }
}