using System;
using System.IO;
using MediatR;

namespace Sher.Application.Commands
{
    public record FileUploadCommand(Guid Id, string Slug, string OriginalFileName, Stream FileStream) : IRequest;
}