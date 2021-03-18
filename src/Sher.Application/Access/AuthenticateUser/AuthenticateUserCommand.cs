using Sher.Application.Processing;

namespace Sher.Application.Access.AuthenticateUser
{
    public record AuthenticateUserCommand(string EmailAddress, string Password) : ICommand<string>;
}