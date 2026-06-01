using AutoMapper;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.User;
using TaskHub.Core.Entities;
using MediatR;
using TaskHub.Application.Interfaces;

namespace TaskHub.Application.Services.UserService.Auth.Command.SignIn
{
    public class SignInHandler : IRequestHandler<SignInCommand, Results<UserDTO>>
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        public SignInHandler(IUserRepository<User> userRepository, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<Results<UserDTO>> Handle(SignInCommand command, CancellationToken ct)
        {
            var email = command.Email.Trim().ToLower();

            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || string.IsNullOrWhiteSpace(user.Password))
            {
                return Results<UserDTO>.Fail("Невірний email або пароль");
            }

            var isPasswordValid = _passwordHasher.Verify(command.Password, user.Password);

            if (!isPasswordValid)
            {
                return Results<UserDTO>.Fail("Невірний email або пароль");
            }

            return Results<UserDTO>.Ok(_mapper.Map<UserDTO>(user));
        }
    }
}
