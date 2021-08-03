using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Files;
using Sher.Application.Files.GetUploader;

namespace Sher.Infrastructure.Tus
{
    public class TusDiskStorePathProvider : IUploaderFileStorePathProvider
    {
        private const string DefaultBaseTusDiskStorePath = "wwwroot/u/";

        private readonly IMediator _mediator;
        private readonly string _basePath;

        public TusDiskStorePathProvider(
            IMediator mediator,
            string basePath)
        {
            _mediator = mediator;
            _basePath = basePath ?? DefaultBaseTusDiskStorePath;
        }

        public async Task<string> GetOrCreateFileStorePathForUserOfId(string userId)
        {
            if (userId is null)
            {
                return _basePath;
            }

            var guidUserId = Guid.Parse(userId);

            var uploader = await _mediator.Send(new GetUploaderQuery(guidUserId));
            var path = GetOrCreateFileStorePathForUploaderOfId(uploader.Id.ToString());

            return path;
        }

        public string GetOrCreateFileStorePathForUploaderOfId(string uploaderId)
        {
            var path = Path.Combine(_basePath, uploaderId);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}