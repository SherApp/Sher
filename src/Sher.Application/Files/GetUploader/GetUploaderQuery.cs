using System;
using MediatR;

namespace Sher.Application.Files.GetUploader
{
    public record GetUploaderQuery(Guid UserId) : IRequest<UploaderDto>;
}