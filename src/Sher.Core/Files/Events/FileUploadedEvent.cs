using System;
using System.IO;
using Sher.Core.Base;

namespace Sher.Core.Files.Events
{
    public class FileUploadedEvent : BaseDomainEvent
    {
        public Guid FileId { get; }
        public string FileName { get; }
        public Stream FileStream { get; }

        public FileUploadedEvent(Guid fileId, string fileName, Stream fileStream)
        {
            FileId = fileId;
            FileName = fileName;
            FileStream = fileStream;
        }
    }
}