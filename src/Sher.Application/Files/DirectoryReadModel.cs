using System;
using System.Collections.Generic;
using Sher.Application.Files.GetUploaderFiles;

namespace Sher.Application.Files
{
    public class DirectoryReadModel : DirectorySummary
    {
        public List<FileDto> Files { get; set; }
        public List<DirectorySummary> Directories { get; set; }

        public DirectoryReadModel(Guid id, Guid uploaderId, string name) : base(id, uploaderId, name)
        {
        }
    }

    public class DirectorySummary
    {
        public Guid Id { get; set; }
        public Guid UploaderId { get; set; }
        public string Name { get; set; }

        public DirectorySummary(Guid id, Guid uploaderId, string name)
        {
            Id = id;
            UploaderId = uploaderId;
            Name = name;
        }
    }
}