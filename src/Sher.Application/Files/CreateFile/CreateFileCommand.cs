using System;
using Sher.Application.Processing;

namespace Sher.Application.Files.CreateFile
{
    public record CreateFileCommand(Guid Id, Guid? DirectoryId, Guid UserId, string FileName, long FileLength) : ICommand;
}