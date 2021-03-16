using System;
using System.Threading.Tasks;

namespace Sher.Core.Access
{
    public class AuthenticationService
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

        public async Task<bool> AuthenticateUserAsync(string emailAddress, string password)
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

            if (!user.IsDeleted) return false;
            
            var isValidPassword =
                await _passwordHashingService.VerifyPasswordAsync(user.Password.Hash, password, user.Password.Salt);

            return isValidPassword;
        }
    }
}