using System;
using System.IO;

namespace Sher.Core.Interfaces
{
    public interface IFileProcessingQueue
    {
        void QueueFile(Stream stream, string fileName, Action<IFileProcessingContext> onSuccess);
    }
}