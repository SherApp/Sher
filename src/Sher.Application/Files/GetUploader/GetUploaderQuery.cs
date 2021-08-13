using System;
using Sher.Application.Processing;

namespace Sher.Application.Files.GetUploader
{
    public record GetUploaderQuery(Guid UserId) : IQuery<UploaderDto>;
}