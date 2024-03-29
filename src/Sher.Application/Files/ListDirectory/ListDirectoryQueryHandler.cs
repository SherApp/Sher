using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Sher.Application.Configuration;
using Sher.Application.Files.GetUploaderFiles;
using Sher.Application.Processing;

namespace Sher.Application.Files.ListDirectory
{
    public class ListDirectoryQueryHandler : IQueryHandler<ListDirectoryQuery, DirectoryDto>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ListDirectoryQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _dbConnectionFactory = connectionFactory;
        }
        
        public Task<DirectoryDto> Handle(ListDirectoryQuery request, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            DirectoryDto mainDir = null;

            var descDirs = new Dictionary<Guid, DirectoryDto>();
            var files = new Dictionary<Guid, FileDto>();

            var (directoryId, userId) = request;
            connection.Query<DirectoryDto, FileDto, DirectoryDto, DirectoryDto>(
                @"SELECT D.*, F.*, C.* FROM ""Uploaders"" UP 
                  INNER JOIN ""Users"" U on U.""Id"" = UP.""UserId""
                  INNER JOIN ""Directory"" D on D.""UploaderId"" = UP.""Id""
                  LEFT JOIN ""Directory"" C on C.""ParentDirectoryId"" = D.""Id"" AND C.""IsDeleted"" = FALSE
                  LEFT JOIN ""Files"" F on F.""DirectoryId"" = D.""Id"" AND F.""IsDeleted"" = FALSE
                      WHERE U.""Id"" = @UserId
                      AND (D.""Id"" = @DirectoryId 
                      OR (D.""ParentDirectoryId"" IS NULL AND @DirectoryId IS NULL))",
                (directoryDto, fileDto, childDirDto) =>
                {
                    if (fileDto is not null)
                    {
                        files[fileDto.Id] = fileDto;
                    }

                    if (childDirDto is not null)
                    {
                        descDirs[childDirDto.Id] = childDirDto;
                    }

                    mainDir = directoryDto;

                    return directoryDto;
                }, new {UserId = userId, DirectoryId = directoryId});
            
            if (mainDir is null)
            {
                return Task.FromResult(mainDir);
            }

            mainDir.Files.AddRange(files.Values);
            mainDir.Directories.AddRange(descDirs.Values);

            var parents = connection.Query<DirectorySummaryDto>(
                @"WITH RECURSIVE parents AS
                  (
                      SELECT ""Id"", ""ParentDirectoryId"", ""Name""
                      FROM ""Directory""
                      WHERE ""Id"" = @DirectoryId
                      UNION
                      SELECT d.""Id"", d.""ParentDirectoryId"", d.""Name""
                      FROM ""Directory"" d
                               INNER JOIN parents p ON p.""ParentDirectoryId"" = d.""Id""
                  )
                  SELECT * FROM parents;", new { DirectoryId = mainDir.Id });
            
            mainDir.Path.AddRange(parents);

            return Task.FromResult(mainDir);
        }
    }
}