using System.Collections.Generic;
using MediatR;

namespace Sher.Application.Files.GetUsersFiles
{
    public record GetUserFilesQuery(string UserId) : IRequest<List<FileDto>>;
}