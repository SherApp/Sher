using System;
using Sher.Core.Access.Users;

namespace Sher.UnitTests.Builders
{
    public class UserBuilder
    {
        private Guid _id = Guid.NewGuid();
        private Password _password = new("somehash", "somesalt");
        private string _emailAddress = "test@example.com";
        private string _refreshToken;

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

        public UserBuilder WithDefaultClient(string refreshToken)
        {
            _refreshToken = refreshToken;
            return this;
        }

        public User Build()
        {
            var user = new User(_id, _emailAddress, _password);

            if (_refreshToken is not null)
            {
                user.CreateClient(_refreshToken);
            }

            return user;
        }
    }
}