using System;
using System.IO;
using Sher.Application.Processing;

namespace Sher.Application.Files.UploadFile
{
    public record UploadFileCommand
        (Guid Id, Guid DirectoryId, Guid UploaderId, string FileName, Stream FileStream) : ICommand;
}