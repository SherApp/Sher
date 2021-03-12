using System.Collections.Generic;
using MediatR;

namespace Sher.Application.Files.GetUsersFiles
{
    public record GetUserFilesQuery(string UserId, string RequiredFileNamePart) : IRequest<List<FileDto>>;
}