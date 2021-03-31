using System;
using System.Threading.Tasks;
using Sher.Core.Access.Users;

namespace Sher.Core.Access
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(
            IPasswordHashingService passwordHashingService,
            IUserRepository userRepository)
        {
            _passwordHashingService = passwordHashingService;
            _userRepository = userRepository;
        }

        public async Task<UserDescriptor> AuthenticateUserAsync(string emailAddress, string password)
        {
            if (emailAddress is null)
            {
                throw new ArgumentNullException(nameof(emailAddress));
            }

            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            
            var user = await _userRepository.GetByEmailAddressAsync(emailAddress);

            if (user is null || user.IsDeleted) return null;
            
            var isValidPassword =
                await _passwordHashingService.VerifyPasswordAsync(user.Password.Hash, password, user.Password.Salt);

            if (!isValidPassword) return null;

            user.SetRefreshToken(_passwordHashingService.GetRandomToken(512 / 8));

            return new UserDescriptor
            {
                NameIdentifier = user.Id.ToString(),
                RefreshToken = user.RefreshToken,
                Role = user.Roles[^1]?.Name
            };
        }

        public async Task<UserDescriptor> RefreshUserTokenAsync(Guid userId, string refreshToken)
        {
            if (refreshToken is null)
            {
                throw new ArgumentNullException(nameof(refreshToken));
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user.IsDeleted || user.RefreshToken != refreshToken)
            {
                return null;
            }

            user.SetRefreshToken(_passwordHashingService.GetRandomToken(512 / 8));

            return new UserDescriptor
            {
                NameIdentifier = user.Id.ToString(),
                RefreshToken = user.RefreshToken,
                Role = user.Roles[^1]?.Name
            };
        }
    }

    public class UserDescriptor
    {
        public string NameIdentifier { get; init; }
        public string RefreshToken { get; init; }
        public string Role { get; init; }
    }
}