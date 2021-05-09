using System;
using Sher.Application.Processing;

namespace Sher.Application.Files.DeleteFile
{
    public record DeleteFileCommand(Guid FileId, Guid UserId) : ICommand<bool>;
}