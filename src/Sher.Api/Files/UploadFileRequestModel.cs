using System;
using Microsoft.AspNetCore.Http;

namespace Sher.Api.Files
{
    public class UploadFileRequestModel
    {
        public Guid Id { get; set; }
        public IFormFile File { get; set; }
    }
}