using System;
using Sher.Core.Base;

namespace Sher.Core.Access
{
    public class PlatformInstance
    {
        protected int Id { get; }
        public PlatformSettings Settings { get; }

        public PlatformInstance(PlatformSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
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