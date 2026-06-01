using Microsoft.EntityFrameworkCore;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

namespace TaskHub.Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly TaskHubContext _context;

        public UserRepository(TaskHubContext context) 
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id).AsTask();
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<User> AddAsync(User user)
        {
            user.UserId = Guid.NewGuid();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<int> UpdateAsync(Guid id, User user)
        {
            return await _context.Users
                .Where(u => u.UserId == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(u => u.Name, user.Name)
                    .SetProperty(u => u.Email, user.Email)
                    .SetProperty(u => u.Password, user.Password)
                );
        }
        public async Task<int> DeleteAsync(Guid id)
        {
            return await _context.Users
                .Where(u => u.UserId == id)
                .ExecuteDeleteAsync();
        }


        public async Task<User> GetByEmailAndPasswordAsync(string email, string passwordHash)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == passwordHash);

            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
