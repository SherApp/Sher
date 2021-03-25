using Sher.Core.Base;

namespace Sher.Core.Access
{
    public class PlatformSettings : ValueObject
    {
        public string InvitationCode { get; private set; }

        public PlatformSettings(string invitationCode)
        {
            InvitationCode = invitationCode;
        }

        // EF Constructor
        private PlatformSettings()
        {
        }
    }
}