using System;
using Sher.Application.Files.GetUploaderFiles;
using Sher.Application.Processing;

namespace Sher.Application.Files.GetFile
{
    public record GetFileQuery(Guid FileId) : IQuery<FileDto>;
}