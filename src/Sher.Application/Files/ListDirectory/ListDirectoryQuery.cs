using System;
using Sher.Application.Processing;

namespace Sher.Application.Files.ListDirectory
{
    public record ListDirectoryQuery(Guid? DirectoryId, Guid UserId) : IQuery<DirectoryDto>;
}