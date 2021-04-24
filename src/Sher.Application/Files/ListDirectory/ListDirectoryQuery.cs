using System;
using MediatR;

namespace Sher.Application.Files.ListDirectory
{
    public record ListDirectoryQuery(Guid? DirectoryId, Guid UserId) : IRequest<DirectoryDto>;
}