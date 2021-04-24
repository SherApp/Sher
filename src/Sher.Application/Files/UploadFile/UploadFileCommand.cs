using System;
using System.IO;
using Sher.Application.Processing;

namespace Sher.Application.Files.UploadFile
{
    public record UploadFileCommand
        (Guid Id, Guid? DirectoryId, Guid UserId, string FileName, Stream FileStream) : ICommand;
}