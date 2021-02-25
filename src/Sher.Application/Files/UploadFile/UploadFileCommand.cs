using System;
using System.IO;
using MediatR;

namespace Sher.Application.Files.UploadFile
{
    public record UploadFileCommand(Guid Id, string OriginalFileName, Stream FileStream) : IRequest;
}