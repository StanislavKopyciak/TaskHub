using TaskHub.Application.DTO.User;

namespace TaskHub.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken();
        Task<AuthResult> RefreshAsync(string refreshToken, CancellationToken ct);
    }
}
