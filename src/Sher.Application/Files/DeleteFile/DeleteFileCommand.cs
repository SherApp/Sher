using System;
using MediatR;

namespace Sher.Application.Files.DeleteFile
{
    public record DeleteFileCommand(Guid FileId, string UserId) : IRequest;
}