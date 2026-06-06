using MediatR;
using Microsoft.AspNetCore.Http;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.User;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

namespace TaskHub.Application.Services.UserService.Auth.Command.VerifyEmail
{
    public class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand, Results<AuthResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailVerificationRepository _verificationRepository;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenRepository _refreshRepos;
        private readonly IRefreshTokenService _refreshTokenService;

        public VerifyEmailHandler(
            IUserRepository userRepository,
            IEmailVerificationRepository verificationRepository,
            IJwtService jwtService,
            IRefreshTokenRepository refreshRepos,
            IRefreshTokenService refreshTokenService)
        {
            _userRepository = userRepository;
            _verificationRepository = verificationRepository;
            _jwtService = jwtService;
            _refreshRepos = refreshRepos;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Results<AuthResult>> Handle(VerifyEmailCommand cmd, CancellationToken ct)
        {
            var verification = await _verificationRepository.GetByCodeAndIdAsync(cmd.Code, cmd.Id, ct);

            if (verification == null)
                return Results<AuthResult>.Fail("Invalid code");

            if (verification.Expiration < DateTime.UtcNow)
                return Results<AuthResult>.Fail("Code expired");

            var user = await _userRepository.GetByIdAsync(verification.UserId, ct);

            if (user == null)
                return Results<AuthResult>.Fail("User not found");

            user.EmailVerified = true;
            await _userRepository.UpdateAsync(user, ct);

            await _verificationRepository.DeleteByUserIdAsync(user.UserId, ct);

            var accesToken = _jwtService.GenerateAccessToken(user.UserId);
            var refreshToken = _refreshTokenService.GenerateRefreshToken();

            var refresh = new RefreshToken
            {
                UserId = user.UserId,
                Token = refreshToken
            };

            _ = await _refreshRepos.AddAsync(refresh, ct);

            return Results<AuthResult>.Ok(new AuthResult
            {
                RefreshToken = refreshToken,
                AccessToken = accesToken
            });
        }
    }
}
