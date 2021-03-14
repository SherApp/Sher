using System;
using System.Linq.Expressions;
using NSpecifications;
using Sher.Core.Files;

namespace Sher.Application.Files.GetUploaderFiles
{
    public class FileSearchCriteria : ASpec<File>
    {
        public string RequiredFileNamePart { get; set; }
        public Guid UploaderId { get; set; }

        public override Expression<Func<File, bool>> Expression => file =>
            (RequiredFileNamePart == null || file.FileName.ToLower().Contains(RequiredFileNamePart.ToLower())) &&
            (UploaderId == null || file.UploaderId.Value == UploaderId);
    }
}