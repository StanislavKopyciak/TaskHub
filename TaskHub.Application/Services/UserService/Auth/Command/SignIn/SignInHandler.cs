using AutoMapper;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.User;
using TaskHub.Core.Entities;
using TaskHub.Core.Interfaces;
using MediatR;

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
            var userGet = await _userRepository.GetByEmailAsync(command.Email);

            if (userGet == null || string.IsNullOrEmpty(userGet.Password))
            {
                return Results<UserDTO>.Fail("Невірний email або пароль");
            }
            if (!_passwordHasher.Verify(command.Password, userGet.Password))
            {
                return Results<UserDTO>.Fail("Невірний email або пароль");
            }

            return Results<UserDTO>.Ok(_mapper.Map<UserDTO>(userGet));
        }
    }
}
