using Sher.Application.Models;

namespace Sher.Application.Interfaces
{
    public interface IFileService
    {
        public void UploadFile(UploadFileModel uploadFileModel);
    }
}