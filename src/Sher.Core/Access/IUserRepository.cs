using System;
using System.Threading.Tasks;

namespace Sher.Core.Access
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetByEmailAddressAsync(string emailAddress);
    }
}