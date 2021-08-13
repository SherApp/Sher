using System;
using System.Collections.Generic;
using Sher.Application.Processing;

namespace Sher.Application.Files.GetUploaderFiles
{
    public record GetUploaderFilesQuery(Guid UserId, string RequiredFileNamePart) : IQuery<List<FileDto>>;
}