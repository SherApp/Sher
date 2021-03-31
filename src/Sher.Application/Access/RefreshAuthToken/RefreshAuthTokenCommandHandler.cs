using System.Threading;
using System.Threading.Tasks;
using Sher.Application.Access.AuthenticateUser;
using Sher.Application.Processing;
using Sher.Core.Access;

namespace Sher.Application.Access.RefreshAuthToken
{
    public class RefreshAuthTokenCommandHandler : ICommandHandler<RefreshAuthTokenCommand, AuthenticationResult>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly JwtIssuer _jwtIssuer;

        public RefreshAuthTokenCommandHandler(
            IAuthenticationService authenticationService,
            JwtIssuer jwtIssuer)
        {
            _authenticationService = authenticationService;
            _jwtIssuer = jwtIssuer;
        }

        public async Task<AuthenticationResult> Handle(RefreshAuthTokenCommand request, CancellationToken cancellationToken)
        {
            var (userId, refreshToken) = request;
            var result = await _authenticationService.RefreshUserTokenAsync(userId, refreshToken);

            if (result is null)
            {
                return null;
            }

            var token = _jwtIssuer.IssueToken(result.NameIdentifier, result.Role);

            return new AuthenticationResult
            {
                JwtToken = token,
                RefreshToken = result.RefreshToken
            };
        }
    }
}