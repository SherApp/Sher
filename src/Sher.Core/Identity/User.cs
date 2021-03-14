using System;
using Sher.Core.Base;

namespace Sher.Core.Identity
{
    public class User : BaseEntity
    {
        public Guid Id { get; }
        public string EmailAddress { get; }
        public string Password { get; }

        public User(Guid id, string emailAddress, string password)
        {
            Id = id;
            EmailAddress = emailAddress;
            Password = password;
        }
    }
}