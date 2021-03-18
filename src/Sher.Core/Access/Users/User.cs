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

        public User(Guid id, string emailAddress, Password password)
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
            if (_roles.First(r => r.Name == role.Name) is not null)
            {
                throw new BusinessRuleViolationException("User cannot have two same roles");
            }
            _roles.Add(role);
        }
    }
}