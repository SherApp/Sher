using System;
using Sher.Core.Base;

namespace Sher.Core.Access
{
    public class PlatformInstance
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

        public User RegisterUser(string invitationCode, Guid id, string emailAddress, string password)
        {
            if (invitationCode != Settings.InvitationCode)
            {
                throw new BusinessRuleViolationException("Invalid invitation code.");
            }

            return new User(id, emailAddress, password);
        }
    }
}