using System;

namespace Sher.Api.Controllers.Files
{
    public class CreateDirectoryRequestModel
    {
        public Guid Id { get; set; }
        public Guid? ParentDirectoryId { get; set; }
        public string Name { get; set; }
    }
}