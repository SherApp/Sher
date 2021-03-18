using System;
using System.Threading.Tasks;

namespace Sher.Core.Access
{
    public interface IAuthenticationService
    {
        public Task<UserDescriptor> AuthenticateUserAsync(string emailAddress, string password);
        public Task RegisterUserAsync(string invitationCode, Guid userId, string emailAddress, string password);
    }
}