using System;
using Sher.Application.Processing;

namespace Sher.Application.Access.GetUser
{
    public record GetUserQuery(Guid UserId) : IQuery<UserDto>;
}