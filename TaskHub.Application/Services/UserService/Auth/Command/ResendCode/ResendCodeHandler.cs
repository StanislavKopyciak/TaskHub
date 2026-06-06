using MediatR;
using System.Security.Cryptography;
using TaskHub.Application.Common;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

namespace TaskHub.Application.Services.UserService.Auth.Command.ResendCode
{
    public class ResendCodeHandler : IRequestHandler<ResendCodeCommand, Results<bool>>
    {
        private readonly IEmailVerificationRepository _verificationRepository;
        private readonly IRefreshTokenService _refreshService;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public ResendCodeHandler (IEmailVerificationRepository verificationRepository, IRefreshTokenService refreshService, IEmailService emailService, IUserRepository userRepository)
        {
            _verificationRepository = verificationRepository;
            _refreshService = refreshService;
            _emailService = emailService;
            _userRepository = userRepository;
        }

        public async Task<Results<bool>> Handle(ResendCodeCommand command, CancellationToken ct) {
            var user = await _userRepository.GetByIdAsync(command.UserId, ct);

            if (user == null)
                return Results<bool>.Fail("user not found");

            if (user.EmailVerified)
                return Results<bool>.Fail("email already verified");

            var lastCode = await _verificationRepository.GetLastByUserIdAsync(command.UserId, ct);

            if (lastCode != null && lastCode.CreatedAt > DateTime.UtcNow.AddSeconds(-30))
                return Results<bool>.Fail("Please wait before requesting a new code");

            var code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

            await _verificationRepository.DeleteByUserIdAsync(command.UserId, ct);

            await _verificationRepository.AddEmailVerificationAsync(new EmailVerification
            {
                UserId = command.UserId,
                Code = code,
            }, ct);

            await _emailService.SendEmailAsync(
                user.Email,
                "TaskHub",
                $"Your code: {code}",
                ct
            );

            return Results<bool>.Ok(true);
        }
    }
}
