using System;

namespace Sher.Application.Files.DeleteDirectory
{
    public class DirectoryCleanupTask
    {
        public int Id { get; private set; }
        public Guid DirectoryId { get; private set; }
        public bool IsProcessed { get; set; }

        public DirectoryCleanupTask(Guid directoryId)
        {
            DirectoryId = directoryId;
        }
    }
}