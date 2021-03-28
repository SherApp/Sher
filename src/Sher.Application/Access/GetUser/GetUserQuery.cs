using System;
using MediatR;

namespace Sher.Application.Access.GetUser
{
    public record GetUserQuery(Guid UserId) : IRequest<UserDto>;
}