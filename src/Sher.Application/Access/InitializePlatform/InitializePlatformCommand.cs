using System;
using Sher.Application.Processing;

namespace Sher.Application.Access.InitializePlatform
{
    public record InitializePlatformCommand(Guid AdminId, string AdminEmailAddress, string AdminPassword) : ICommand;
}