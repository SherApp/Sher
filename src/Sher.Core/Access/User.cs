using System;
using System.Collections.Generic;
using System.Linq;
using Sher.Core.Base;

namespace Sher.Core.Access
{
    public class User : BaseEntity
    {
        public Guid Id { get; }
        public string EmailAddress { get; }
        public string Password { get; }
        public IReadOnlyList<UserRole> Roles => _roles.AsReadOnly();
        private List<UserRole> _roles = new();

        public User(Guid id, string emailAddress, string password)
        {
            Id = id;
            EmailAddress = emailAddress;
            Password = password;
        }

        public void AssignRole(UserRole role)
        {
            if (_roles.First(r => r.Name == role.Name) is not null)
            {
                throw new BusinessRuleViolationException("User cannot have two same roles");
            }
            _roles.Add(role);
        }
    }
}