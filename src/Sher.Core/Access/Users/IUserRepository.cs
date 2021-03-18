using System;
using System.Threading.Tasks;

namespace Sher.Core.Access.Users
{
    public interface IUserRepository
    {
        Task<Users.User> GetUserByIdAsync(Guid id);
        Task<Users.User> GetByEmailAddressAsync(string emailAddress);
        Task AddUserAsync(Users.User user);
    }
}