using Sher.Core.Base;

namespace Sher.Core.Access
{
    public class UserRole : ValueObject
    {
        public string Name { get; }

        public static string Admin = "Admin";
    }
}