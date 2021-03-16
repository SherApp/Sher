using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sher.Core.Access;

namespace Sher.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;

        public UserRepository(AppDbContext dbContext)
        {
            _users = dbContext.Set<User>();
        }

        public Task<User> GetUserByIdAsync(Guid id)
        {
            return _users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<User> GetByEmailAddressAsync(string emailAddress)
        {
            return _users.FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);
        }
    }
}