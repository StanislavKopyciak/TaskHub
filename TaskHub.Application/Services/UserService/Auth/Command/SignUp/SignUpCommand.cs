using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.User;

namespace TaskHub.Application.Services.UserService.Auth.Command.SignUp
{
    public class SignUpCommand : IRequest<Results<UserDTO>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
