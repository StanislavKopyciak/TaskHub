using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

namespace TaskHub.Infrastructure.Data.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly TaskHubContext _context;

        public RefreshTokenRepository (TaskHubContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> AddAsync(RefreshToken task, CancellationToken ct)
        {
            await _context.RefreshTokens.AddAsync(task, ct);
            await _context.SaveChangesAsync(ct);
            return task;
        }

        public async Task<int> DeleteAsync(Guid userId, CancellationToken ct)
        {
            return await _context.RefreshTokens
                .Where(u => u.UserId == userId)
                .ExecuteDeleteAsync(ct);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(i => i.Token == token, ct);
        }

        public async Task<int> UpdateAsync(RefreshToken task, CancellationToken ct)
        {
            return await _context.RefreshTokens.Where(i => i.Id == task.Id).ExecuteUpdateAsync(i => i
                .SetProperty(i => i.UserId, task.UserId)
                .SetProperty(i => i.Token, task.Token)
                .SetProperty(i => i.Expires, task.Expires), ct);
        }
    }
}
