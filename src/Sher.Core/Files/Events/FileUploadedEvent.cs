using System;
using System.IO;
using Sher.Core.Base;

namespace Sher.Core.Files.Events
{
    public class FileUploadedEvent : BaseDomainEvent
    {
        public Guid FileId { get; }
        public Guid DirectoryId { get; }
        public Guid UploaderId { get; }
        public string FileName { get; }
        public Stream FileStream { get; }

        public FileUploadedEvent(Guid fileId, Guid directoryId, Guid uploaderId, string fileName, Stream fileStream)
        {
            FileId = fileId;
            DirectoryId = directoryId;
            UploaderId = uploaderId;
            FileName = fileName;
            FileStream = fileStream;
        }
    }
}