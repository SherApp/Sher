using System;

namespace Sher.Application.Files.ListDirectory
{
    public class DirectorySummaryDto
    {
        public Guid Id { get; set; }
        public Guid? ParentDirectoryId { get; set; }
        public string Name { get; set; }
    }
}