using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.User;
using TaskHub.Application.Interfaces;

namespace TaskHub.Application.Services.UserService.Auth.Command.SignIn
{
    public class SignInHandler : IRequestHandler<SignInCommand, Results<AuthResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenRepository _refreshRepository; 
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;
        public SignInHandler(
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher, 
            IJwtService jwtService, 
            IRefreshTokenRepository 
            refreshRepository, 
            IRefreshTokenService refreshTokenService
            )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _refreshRepository = refreshRepository;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Results<AuthResult>> Handle(SignInCommand command, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(command.Email) || string.IsNullOrWhiteSpace(command.Password))
            {
                return Results<AuthResult>.Fail("Email and password are requied");
            }

            var email = command.Email.Trim().ToLower();

            var user = await _userRepository.GetByEmailAsync(email, ct);

            if (user == null || string.IsNullOrWhiteSpace(user.Password))
            {
                return Results<AuthResult>.Fail("Incorrect email or password");
            }

            var isPasswordValid = _passwordHasher.Verify(command.Password, user.Password);

            if (!isPasswordValid)
            {
                return Results<AuthResult>.Fail("Incorrect email or password");
            }

            if (!user.EmailVerified)
            {
                return Results<AuthResult>.Fail("Email is not verified");
            }

            var accesToken = _jwtService.GenerateAccessToken(user.UserId);
            var refreshToken = _refreshTokenService.GenerateRefreshToken();

            var refresh = new Core.Entities.RefreshToken
            {
                UserId = user.UserId,
                Token = refreshToken
            };

            _ = await _refreshRepository.AddAsync(refresh, ct);

            return Results<AuthResult>.Ok(new AuthResult
            {
                RefreshToken = refreshToken,
                AccessToken = accesToken
            });
        }
    }
}