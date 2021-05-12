using System;
using Sher.Application.Processing;

namespace Sher.Application.Files.DeleteDirectory
{
    public record DeleteDirectoryCommand(Guid DirectoryId, Guid UserId) : ICommand;
}