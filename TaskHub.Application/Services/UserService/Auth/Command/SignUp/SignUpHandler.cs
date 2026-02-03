using AutoMapper;
using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.User;
using TaskHub.Core.Entities;
using TaskHub.Core.Interfaces;

namespace TaskHub.Application.Services.UserService.Auth.Command.SignUp
{
    public class SignUpHandler : IRequestHandler<SignUpCommand, Results<UserDTO>>
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public SignUpHandler(IUserRepository<User> userRepository, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<Results<UserDTO>> Handle(SignUpCommand command, CancellationToken ct)
        {
            var userGet = await _userRepository.GetByEmailAsync(command.Email);

            if (userGet != null)
            {
                return Results<UserDTO>.Fail("Почта Email вже використовується");
            }


            var newUser = new User
            {
                Name = command.Name,
                Email = command.Email,
                Password = _passwordHasher.Hash(command.Password)
            };


            await _userRepository.AddAsync(newUser);

            return Results<UserDTO>.Ok(_mapper.Map<UserDTO>(newUser));
        }
    }
}
