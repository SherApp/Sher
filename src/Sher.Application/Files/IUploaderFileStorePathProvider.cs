using System.Threading.Tasks;

namespace Sher.Application.Files
{
    public interface IUploaderFileStorePathProvider
    {
        Task<string> GetOrCreateFileStorePathForUploaderOfId(string uploaderId);
    }
}