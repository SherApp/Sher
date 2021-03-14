using System;

namespace Sher.Application.Files.GetUploaderFiles
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public int Length { get; set; }
        public bool IsDeleted { get; set; }
    }
}