using System.Security.Cryptography;
using TaskHub.Application.DTO.User;
using TaskHub.Application.Interfaces;

namespace TaskHub.Infrastructure.Services.Auth
{
    public class RefreshTokenService : IRefreshTokenService 
    {
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenRepository _refreshRepos;
        public RefreshTokenService(IRefreshTokenRepository refreshRepos, IJwtService jwtService)
        {
            _refreshRepos = refreshRepos;
            _jwtService = jwtService;
        }

        public async Task<AuthResult> RefreshAsync(string token, CancellationToken ct)
        {
            var refreshToken = await _refreshRepos.GetByTokenAsync(token, ct);

            if (refreshToken is null)
            {
                throw new Exception("The token has expired.");
            }

            if (refreshToken.Expires < DateTime.UtcNow)
            {
                _ = await _refreshRepos.DeleteAsync(refreshToken.UserId, ct);
                throw new Exception("The token has expired.");
            }

            var accessToken = _jwtService.GenerateAccessToken(refreshToken.UserId);

            refreshToken.Token = GenerateRefreshToken();
            refreshToken.Expires = DateTime.UtcNow.AddDays(7);

            await _refreshRepos.UpdateAsync(refreshToken, ct);

            return new AuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}
