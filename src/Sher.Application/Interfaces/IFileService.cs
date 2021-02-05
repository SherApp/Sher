using System.Threading.Tasks;
using Sher.Application.Models;

namespace Sher.Application.Interfaces
{
    public interface IFileService
    {
        public Task UploadFile(UploadFileModel uploadFileModel);
    }
}