using System;

namespace Sher.Core.Access.Users
{
    public class UserClient
    {
        public Guid Id { get; private set; }
        public string RefreshToken { get; private set; }

        public UserClient(Guid id, string refreshToken)
        {
            Id = id;
            RefreshToken = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken));
        }

        internal void UpdateRefreshToken(string newRefreshToken)
        {
            RefreshToken = newRefreshToken;
        }
    }
}