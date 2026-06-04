using Microsoft.EntityFrameworkCore;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

namespace TaskHub.Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskHubContext _context;

        public UserRepository(TaskHubContext context) 
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id, ct);

            return user;
        }

        public async Task<User> AddAsync(User user, CancellationToken ct)
        {
            await _context.Users.AddAsync(user, ct);
            await _context.SaveChangesAsync(ct);
            return user;
        }

        public async Task<int> UpdateAsync(User user, CancellationToken ct)
        {
            return await _context.Users
                .Where(u => u.UserId == user.UserId)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(u => u.Name, user.Name)
                    .SetProperty(u => u.Email, user.Email)
                    .SetProperty(u => u.Password, user.Password)
                    .SetProperty(u => u.EmailVerified, user.EmailVerified), ct
                );
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken ct)
        {
            return await _context.Users
                .Where(u => u.UserId == id)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
            return user;
        }
    }
}
