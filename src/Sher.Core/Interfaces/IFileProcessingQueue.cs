using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Sher.Core.Interfaces
{
    public interface IFileProcessingQueue<in TContext>
    {
        void QueueFile(Stream stream, string fileName, TContext context);
    }
}