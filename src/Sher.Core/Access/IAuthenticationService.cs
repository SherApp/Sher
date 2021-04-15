using System;
using System.Threading.Tasks;

namespace Sher.Core.Access
{
    public interface IAuthenticationService
    {
        public Task<UserDescriptor> AuthenticateUserAsync(string emailAddress, string password);
        public Task<UserDescriptor> RefreshUserTokenAsync(Guid userId, Guid clientId, string refreshToken);
    }
}