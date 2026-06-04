using TaskHub.Core.Entities;

namespace TaskHub.Application.Interfaces
{
    public interface IEmailVerificationRepository
    {
        Task<EmailVerification> GetByCodeAsync(string code, CancellationToken ct);
        Task AddEmailVerificationAsync(EmailVerification emailVerification, CancellationToken ct);
        Task DeleteByUserIdAsync(Guid id, CancellationToken ct);
        Task MarkAsUsed(Guid id, CancellationToken ct);
    }
}
