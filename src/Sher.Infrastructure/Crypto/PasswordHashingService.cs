using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;
using Sher.Core.Access;
using Sher.SharedKernel.Options;

namespace Sher.Infrastructure.Crypto
{
    public class PasswordHashingService : IPasswordHashingService
    {
        private readonly PasswordHashingOptions _options;

        public PasswordHashingService(IOptions<PasswordHashingOptions> options)
        {
            _options = options.Value;
        }

        public async Task<HashResult> HashPasswordAsync(string password)
        {
            var saltBytes = GetRandomBytes(64 / 8);
            return await HashPasswordWithSaltAsync(password, saltBytes);
        }

        private async Task<HashResult> HashPasswordWithSaltAsync(string password, byte[] saltBytes)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            using var argon2 = new Argon2id(passwordBytes)
            {
                Iterations = _options.Iterations,
                DegreeOfParallelism = _options.DegreeOfParallelism,
                MemorySize = _options.MemorySize,
                Salt = saltBytes
            };
            var hashedBytes = await argon2.GetBytesAsync(16);

            return new HashResult
            {
                Hash = Convert.ToBase64String(hashedBytes),
                Salt = Convert.ToBase64String(saltBytes)
            };
        }

        public async Task<bool> VerifyPasswordAsync(string hashedPassword, string password, string salt)
        {
            if (hashedPassword is null)
            {
                throw new ArgumentNullException(nameof(hashedPassword));
            }

            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (salt is null)
            {
                throw new ArgumentNullException(nameof(salt));
            }

            var saltBytes = Convert.FromBase64String(salt);
            var result = await HashPasswordWithSaltAsync(password, saltBytes);
            return result.Hash == hashedPassword;
        }

        public string GetRandomToken(int size)
        {
            return Convert.ToBase64String(GetRandomBytes(size));
        }

        private static byte[] GetRandomBytes(int size)
        {
            var salt = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return salt;
        }
    }
}