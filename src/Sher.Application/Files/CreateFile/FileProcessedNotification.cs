using System;
using MediatR;

namespace Sher.Application.Files.CreateFile
{
    public record FileProcessedNotification(Guid FileId) : INotification;
}