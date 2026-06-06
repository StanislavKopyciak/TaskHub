using TaskHub.Core.Entities;

namespace TaskHub.Application.Interfaces
{
    public interface IRefreshTokenRepository 
    {
        Task<RefreshToken> GetByTokenAsync(string token, CancellationToken ct);
        Task<RefreshToken> AddAsync(RefreshToken token, CancellationToken ct);
        Task<int> UpdateAsync(RefreshToken token, CancellationToken ct);
        Task<int> DeleteAsync(Guid id, CancellationToken ct);
    }
}
