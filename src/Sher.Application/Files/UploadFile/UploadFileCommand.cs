using System;
using System.IO;
using MediatR;
using Sher.Application.Processing;

namespace Sher.Application.Files.UploadFile
{
    public record UploadFileCommand(Guid Id, Guid UploaderId, string FileName, Stream FileStream) : ICommand;
}