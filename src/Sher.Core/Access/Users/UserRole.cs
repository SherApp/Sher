using Sher.Core.Base;

namespace Sher.Core.Access.Users
{
    public class UserRole : ValueObject
    {
        public string Name { get; private set; }

        public UserRole(string name)
        {
            Name = name;
        }

        private UserRole()
        {
        }

        public static string Admin = "Admin";
    }
}