using System;
using System.Threading.Tasks;
using Sher.Core.Access.Users;

namespace Sher.Core.Access
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IUserRepository _userRepository;
        private readonly IPlatformInstanceRepository _platformInstanceRepository;

        public AuthenticationService(
            IPasswordHashingService passwordHashingService,
            IUserRepository userRepository,
            IPlatformInstanceRepository platformInstanceRepository)
        {
            _passwordHashingService = passwordHashingService;
            _userRepository = userRepository;
            _platformInstanceRepository = platformInstanceRepository;
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

            return new UserDescriptor
            {
                NameIdentifier = user.Id.ToString()
            };
        }
        
        public async Task RegisterUserAsync(string invitationCode, Guid userId, string emailAddress, string password)
        {
            if (emailAddress is null)
            {
                throw new ArgumentNullException(nameof(emailAddress));
            }

            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (invitationCode is null)
            {
                throw new ArgumentNullException(nameof(invitationCode));
            }

            var hashedPassword = await _passwordHashingService.HashPasswordAsync(password);
            var platform = await _platformInstanceRepository.GetPlatformInstance();

            var user = platform.RegisterUser("", userId, emailAddress,
                new Password(hashedPassword.Hash, hashedPassword.Salt));

            await _userRepository.AddUserAsync(user);
        }
    }

    public class UserDescriptor
    {
        public string NameIdentifier { get; init; }
    }
}