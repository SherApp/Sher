using System;
using Sher.Core.Access.Users;

namespace Sher.UnitTests.Builders
{
    public class UserBuilder
    {
        private Guid _id = Guid.NewGuid();
        private Password _password = new("somehash", "somesalt");
        private string _emailAddress = "test@example.com";

        public UserBuilder WithId(Guid userId)
        {
            _id = userId;
            return this;
        }

        public UserBuilder WithPassword(Password password)
        {
            _password = password;
            return this;
        }

        public UserBuilder WithEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
            return this;
        }

        public User Build()
        {
            return new(_id, _emailAddress, _password);
        }
    }
}