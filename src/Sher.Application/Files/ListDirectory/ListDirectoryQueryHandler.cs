using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Sher.Application.Configuration;
using Sher.Application.Files.GetUploaderFiles;

namespace Sher.Application.Files.ListDirectory
{
    public class ListDirectoryQueryHandler : IRequestHandler<ListDirectoryQuery, DirectoryDto>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ListDirectoryQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _dbConnectionFactory = connectionFactory;
        }
        
        public Task<DirectoryDto> Handle(ListDirectoryQuery request, CancellationToken cancellationToken)
        {
            using var connection = _dbConnectionFactory.GetOpenConnection();

            DirectoryDto mainDir = null;

            var descDirs = new Dictionary<Guid, DirectoryDto>();
            var files = new Dictionary<Guid, FileDto>();

            var (directoryId, userId) = request;
            connection.Query<DirectoryDto, FileDto, DirectoryDto>(
                @"SELECT D.*, F.* FROM ""Uploaders"" UP 
                  INNER JOIN ""Users"" U on U.""Id"" = UP.""UserId""
                  INNER JOIN ""Directory"" D on D.""UploaderId"" = UP.""Id""
                  INNER JOIN ""Files"" F on F.""DirectoryId"" = D.""Id""
                      WHERE U.""Id"" = @UserId
                      AND ((D.""ParentDirectoryId"" IS NULL OR D.""ParentDirectoryId"" = @DirectoryId) AND D.""UploaderId"" = UP.""Id"")",
                (directoryDto, fileDto) =>
                {
                    files[fileDto.Id] = fileDto;
                    if (directoryDto.Id == directoryId || directoryDto.ParentDirectoryId is null)
                    {
                        mainDir = directoryDto;
                    }
                    else
                    {
                        descDirs[directoryDto.Id] = directoryDto;
                    }

                    return directoryDto;
                }, new {UserId = userId, DirectoryId = directoryId});

            mainDir?.Directories.AddRange(descDirs.Values);
            mainDir?.Files.AddRange(files.Values);

            return Task.FromResult(mainDir);
        }
    }
}