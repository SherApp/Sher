using System;
using Sher.Application.Access.AuthenticateUser;
using Sher.Application.Processing;

namespace Sher.Application.Access.RefreshAuthToken
{
    public record RefreshAuthTokenCommand(Guid UserId, string RefreshToken) : ICommand<AuthenticationResult>;
}