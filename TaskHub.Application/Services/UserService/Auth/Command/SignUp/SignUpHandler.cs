using AutoMapper;
using MediatR;
using System.Security.Cryptography;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.User;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

namespace TaskHub.Application.Services.UserService.Auth.Command.SignUp
{
    public class SignUpHandler : IRequestHandler<SignUpCommand, Results<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IRefreshTokenService _jwtService;
        private readonly IEmailService _emailService;
        private readonly IEmailVerificationRepository _emailVerificationRepository;

        public SignUpHandler
            (
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher,
            IMapper mapper,
            IRefreshTokenService jwtService,
            IEmailService emailService,
            IEmailVerificationRepository emailVerificationRepository
            )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _jwtService = jwtService;
            _emailService = emailService;
            _emailVerificationRepository = emailVerificationRepository;
            }

        public async Task<Results<Guid>> Handle(SignUpCommand command, CancellationToken ct)
        {
            var email = command.Email.Trim().ToLower();

            var existingUser = await _userRepository.GetByEmailAsync(email, ct);

            if (existingUser != null && existingUser.EmailVerified)
                return Results<Guid>.Fail("Email is already used");

            if (command.Password != command.ConfirmPassword)
                return Results<Guid>.Fail("Passwords do not match");

            User user;

            if (existingUser != null)
            {
                if (existingUser.EmailVerified)
                    return Results<Guid>.Fail("Email is already used");

                existingUser.Name = command.Name;
                existingUser.Password = _passwordHasher.Hash(command.Password);

                await _userRepository.UpdateAsync(existingUser, ct);

                user = existingUser;
            }
            else
            {
                user = new User
                {
                    Name = command.Name,
                    Email = email,
                    Password = _passwordHasher.Hash(command.Password),
                    CreatedAt = DateTime.UtcNow
                };

                await _userRepository.AddAsync(user, ct);
            }

            var code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

            await _emailVerificationRepository.DeleteByUserIdAsync(user.UserId, ct);

            await _emailVerificationRepository.AddEmailVerificationAsync(new EmailVerification
            {
                UserId = user.UserId,
                Code = code,
            }, ct);

            await _emailService.SendEmailAsync(
                email,
                "TaskHub",
                $"Your code: {code}",
                ct
            );

            return Results<Guid>.Ok(user.UserId);
        }
    }
}
