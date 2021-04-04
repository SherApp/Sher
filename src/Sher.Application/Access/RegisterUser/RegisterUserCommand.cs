using System;
using Sher.Application.Processing;

namespace Sher.Application.Access.RegisterUser
{
    public record RegisterUserCommand(Guid UserId, string EmailAddress, string Password, string InvitationCode) : ICommand;
}