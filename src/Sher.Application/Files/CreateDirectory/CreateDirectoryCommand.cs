using System;
using Sher.Application.Processing;

namespace Sher.Application.Files.CreateDirectory
{
    public record CreateDirectoryCommand(Guid DirectoryId, Guid? ParentDirectoryId, Guid UserId, string Name) : ICommand;
}