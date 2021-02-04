using System;

namespace Sher.Application.Models
{
    public class UploadFileModel
    {
        public Guid Id { get; set; }
        public FileModel File { get; set; }
    }
}