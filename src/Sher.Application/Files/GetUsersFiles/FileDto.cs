using System;

namespace Sher.Application.Files.GetUsersFiles
{
    public class FileDto
    {
        public Guid Id { get; set; }
        public string Slug { get; set; }
        public string OriginalFileName { get; set; }
        public int Length { get; set; }
    }
}