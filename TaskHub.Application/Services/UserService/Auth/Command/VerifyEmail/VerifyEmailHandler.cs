using MediatR;
using Microsoft.AspNetCore.Http;
using TaskHub.Application.Common;
using TaskHub.Application.Interfaces;

namespace TaskHub.Application.Services.UserService.Auth.Command.VerifyEmail
{
    public class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand, Results<AuthResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailVerificationRepository _verificationRepository;
        private readonly IJwtService _jwtService;

        public VerifyEmailHandler(
            IUserRepository userRepository,
            IEmailVerificationRepository verificationRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _verificationRepository = verificationRepository;
            _jwtService = jwtService;
        }

        public async Task<Results<AuthResult>> Handle(VerifyEmailCommand cmd, CancellationToken ct)
        {
            var verification = await _verificationRepository.GetByCodeAsync(cmd.Code, ct);

            if (verification == null)
                return Results<AuthResult>.Fail("Invalid code");

            if (verification.IsUsed)
                return Results<AuthResult>.Fail("Code already used");

            if (verification.Expiration < DateTime.UtcNow)
                return Results<AuthResult>.Fail("Code expired");

            var user = await _userRepository.GetByIdAsync(verification.UserId, ct);

            if (user == null)
                return Results<AuthResult>.Fail("User not found");

            user.EmailVerified = true;
            await _userRepository.UpdateAsync(user, ct);


            verification.IsUsed = true;
            await _verificationRepository.MarkAsUsed(verification.Id, ct);


            await _verificationRepository.DeleteByUserIdAsync(user.UserId, ct);

            var jwt = _jwtService.GenerateToken(user.UserId);

            return Results<AuthResult>.Ok(new AuthResult
            {
                Token = jwt
            });
        }
    }
}
