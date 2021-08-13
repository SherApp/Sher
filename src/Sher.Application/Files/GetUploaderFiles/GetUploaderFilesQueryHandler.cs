using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Sher.Application.Configuration;
using Sher.Application.Processing;

namespace Sher.Application.Files.GetUploaderFiles
{
    public class GetUploaderFilesQueryHandler : IQueryHandler<GetUploaderFilesQuery, List<FileDto>>
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public GetUploaderFilesQueryHandler(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task<List<FileDto>> Handle(GetUploaderFilesQuery request, CancellationToken cancellationToken)
        {
            var connection = _connectionFactory.GetOpenConnection();
            var (userId, requiredFileNamePart) = request;

            var files = connection.Query<FileDto>(@"SELECT F.* FROM ""Uploaders"" UP
                                INNER JOIN ""Users"" U on U.""Id"" = UP.""UserId""
                                INNER JOIN ""Files"" F on F.""UploaderId"" = UP.""Id""
                                WHERE U.""Id"" = @UserId
                                AND F.""IsDeleted"" = FALSE
                                AND POSITION(UPPER(@RequiredFileNamePart) in UPPER(F.""FileName"")) > 0",
                new { UserId = userId, RequiredFileNamePart = requiredFileNamePart });

            return Task.FromResult(files.ToList());
        }
    }
}