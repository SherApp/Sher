namespace Sher.Application.Files
{
    public interface IUploaderFileStorePathProvider
    {
        string GetOrCreateFileStorePathForUploaderOfId(string uploaderId);
    }
}