using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Sher.Application.Configuration;
using Sher.Application.Processing;

namespace Sher.Application.Files.GetUploader
{
    public class GetUploaderQueryHandler : IQueryHandler<GetUploaderQuery, UploaderDto>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetUploaderQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public Task<UploaderDto> Handle(GetUploaderQuery request, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();
            var uploader = connection.QuerySingle<UploaderDto>(
                @"SELECT * FROM ""Uploaders"" WHERE ""UserId"" = @UserId", 
                new
            {
                request.UserId
            });

            return Task.FromResult(uploader);
        }
    }
}