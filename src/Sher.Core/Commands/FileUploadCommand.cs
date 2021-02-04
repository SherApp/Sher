using System;
using System.IO;
using MediatR;

namespace Sher.Core.Commands
{
    public record FileUploadCommand(Guid Id, string FileName, string OriginalFileName, Stream FileStream) : IRequest;
}