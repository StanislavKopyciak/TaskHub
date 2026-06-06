using TaskHub.Core.Entities;

namespace TaskHub.Application.Interfaces
{
    public interface IEmailVerificationRepository
    {
        Task<EmailVerification> GetByCodeAndIdAsync(string code, Guid id, CancellationToken ct);
        Task AddEmailVerificationAsync(EmailVerification emailVerification, CancellationToken ct);
        Task DeleteByUserIdAsync(Guid id, CancellationToken ct);
        Task MarkAsUsed(Guid id, CancellationToken ct);

        Task<EmailVerification> GetLastByUserIdAsync(Guid userId, CancellationToken ct);
    }
}
