using System.Threading.Tasks;

namespace Sher.Core.Access
{
    public interface IPasswordHashingService
    {
        Task<HashResult> HashPasswordAsync(string password);
        Task<bool> VerifyPasswordAsync(string hashedPassword, string password, string salt);
        string GetRandomToken(int size);
    }
}