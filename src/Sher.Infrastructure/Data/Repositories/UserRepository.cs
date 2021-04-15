using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sher.Core.Access.Users;

namespace Sher.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;
        private IQueryable<User> Queryable => _users.Include(u => u.Clients);

        public UserRepository(AppDbContext dbContext)
        {
            _users = dbContext.Set<User>();
        }

        public Task<User> GetUserByIdAsync(Guid id)
        {
            return Queryable.FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<User> GetByEmailAddressAsync(string emailAddress)
        {
            return Queryable.FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);
        }

        public async Task AddUserAsync(User user)
        {
            await _users.AddAsync(user);
        }
    }
}