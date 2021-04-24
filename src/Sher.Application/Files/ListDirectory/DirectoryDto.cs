using System;
using System.Collections.Generic;
using Sher.Application.Files.GetUploaderFiles;

namespace Sher.Application.Files.ListDirectory
{
    public class DirectoryDto
    {
        public Guid Id { get; set; }
        public Guid? ParentDirectoryId { get; set; }
        public string Name { get; set; }
        public List<FileDto> Files { get; set; } = new();
        public List<DirectoryDto> Directories { get; set; } = new();
    }
}