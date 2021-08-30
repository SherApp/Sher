using System;
using System.Collections.Generic;
using System.Linq;
using Sher.Core.Base;

namespace Sher.Core.Access.Users
{
    public class User : BaseEntity
    {
        public Guid Id { get; }
        public string EmailAddress { get; private set; }
        public Password Password { get; private set; }
        public IReadOnlyList<UserRole> Roles => _roles.AsReadOnly();
        private List<UserRole> _roles = new();
        public IReadOnlyList<UserClient> Clients => _clients.AsReadOnly();
        private List<UserClient> _clients = new();
        public bool IsDeleted { get; private set; }

        internal User(Guid id, string emailAddress, Password password)
        {
            Id = id;
            EmailAddress = emailAddress;
            Password = password;
        }

        // EF Core constructor
        private User()
        {
        }

        public void AssignRole(UserRole role)
        {
            if (_roles.FirstOrDefault(r => r.Name == role.Name) is not null)
            {
                throw new BusinessRuleViolationException("User cannot have two same roles");
            }
            _roles.Add(role);
        }

        public UserClient CreateClient(string refreshToken)
        {
            var client = new UserClient(Guid.NewGuid(), refreshToken);
            _clients.Add(client);

            return client;
        }

        public void DeleteClient(Guid clientId)
        {
            _clients.RemoveAll(c => c.Id == clientId);
        }

        public bool UpdateClientRefreshToken(Guid clientId, string previousRefreshToken, string newRefreshToken)
        {
            var client = _clients.FirstOrDefault(c => c.Id == clientId);

            if (client is null) return false;
            if (client.RefreshToken != previousRefreshToken) return false;
            
            client.UpdateRefreshToken(newRefreshToken);

            return true;
        }
    }
}