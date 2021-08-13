using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Sher.Application.Configuration;
using Sher.Application.Files.GetUploaderFiles;
using Sher.Application.Processing;

namespace Sher.Application.Files.GetFile
{
    public class GetFileQueryHandler : IQueryHandler<GetFileQuery, FileDto>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetFileQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public Task<FileDto> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            var file = connection.QuerySingleOrDefault<FileDto>(
                @"SELECT * FROM ""Files"" WHERE ""Id"" = @FileId AND ""IsDeleted"" = FALSE", 
                new {request.FileId});

            return Task.FromResult(file);
        }
    }
}