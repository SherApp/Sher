using System;
using MediatR;
using Sher.Application.Files.GetUploaderFiles;

namespace Sher.Application.Files.GetFile
{
    public record GetFileQuery(Guid FileId) : IRequest<FileDto>;
}