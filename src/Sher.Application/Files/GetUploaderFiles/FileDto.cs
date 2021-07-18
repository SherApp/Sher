using System;

namespace Sher.Application.Files.GetUploaderFiles
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public Guid UploaderId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public int Length { get; set; }
        public bool IsDeleted { get; set; }
    }
}