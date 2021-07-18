using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Sher.Application.Configuration;

namespace Sher.Application.Files.GetUploaderFiles
{
    public class GetUploaderFilesQueryHandler : IRequestHandler<GetUploaderFilesQuery, List<FileDto>>
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
                                WHERE U.""Id"" = @UserId AND POSITION(UPPER(@RequiredFileNamePart) in UPPER(F.""FileName"")) > 0",
                new { UserId = userId, RequiredFileNamePart = requiredFileNamePart });

            return Task.FromResult(files.ToList());
        }
    }
}