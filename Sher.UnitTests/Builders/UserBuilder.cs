using System;
using Sher.Core.Access.Users;

namespace Sher.UnitTests.Builders
{
    public class UserBuilder
    {
        private Guid _id = Guid.NewGuid();
        private Password _password = new("somehash", "somesalt");
        private string _emailAddress = "test@example.com";
        private string _refreshToken = "token";

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

        public UserBuilder WithRefreshToken(string refreshToken)
        {
            _refreshToken = refreshToken;
            return this;
        }

        public User Build()
        {
            var user = new User(_id, _emailAddress, _password);
            user.SetRefreshToken(_refreshToken);

            return user;
        }
    }
}