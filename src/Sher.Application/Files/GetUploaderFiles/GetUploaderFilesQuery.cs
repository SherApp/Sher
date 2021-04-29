using System;
using System.Collections.Generic;
using MediatR;

namespace Sher.Application.Files.GetUploaderFiles
{
    public record GetUploaderFilesQuery(Guid UserId, string RequiredFileNamePart) : IRequest<List<FileDto>>;
}