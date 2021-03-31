using System;
using System.Threading.Tasks;
using Sher.Core.Access.Rules;
using Sher.Core.Access.Users;
using Sher.Core.Base;

namespace Sher.Core.Access
{
    public class PlatformInstance : BaseEntity
    {
        private int _id;
        public PlatformSettings Settings { get; private set; }

        public PlatformInstance(PlatformSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        // EF Core constructor
        private PlatformInstance()
        {
        }

        public async Task<User> RegisterUser(
            Guid id,
            string emailAddress,
            string password,
            IPasswordHashingService hashingService,
            string invitationCode = null)
        {
            if (invitationCode != Settings.InvitationCode)
            {
                throw new BusinessRuleViolationException("Invalid invitation code.");
            }

            CheckRule(new PasswordSecurityRule(password));

            var hashResult = await hashingService.HashPasswordAsync(password);
            return new User(id, emailAddress, new Password(hashResult.Hash, hashResult.Salt));
        }

        public void UpdateSettings(string invitationCode)
        {
            Settings = new PlatformSettings(invitationCode);
        }
    }
}