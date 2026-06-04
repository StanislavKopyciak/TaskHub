using Microsoft.EntityFrameworkCore;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

namespace TaskHub.Infrastructure.Data.Repository
{
    public class EmailVerificationRepository : IEmailVerificationRepository
    {
        private readonly TaskHubContext _context;

        public EmailVerificationRepository(TaskHubContext context)
        {
            _context = context;
        }

        public async Task AddEmailVerificationAsync(EmailVerification emailVerification, CancellationToken ct)
        {
            await _context.EmailVerifications.AddAsync(emailVerification, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteByUserIdAsync(Guid id, CancellationToken ct)
        {
            await _context.EmailVerifications.Where(ev => ev.UserId == id).ExecuteDeleteAsync(ct);
        }

        public async Task<EmailVerification?> GetByCodeAsync(string code, CancellationToken ct)
        {
            return await _context.EmailVerifications.FirstOrDefaultAsync(ev => ev.Code == code, ct);
        }

        public async Task MarkAsUsed(Guid id, CancellationToken ct)
        {
            await _context.EmailVerifications
                .Where(ev => ev.Id == id)
                .ExecuteUpdateAsync(ev => ev.SetProperty(e => e.IsUsed, true), ct);
        }
    }
}
