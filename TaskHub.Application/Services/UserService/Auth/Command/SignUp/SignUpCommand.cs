using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.User;

namespace TaskHub.Application.Services.UserService.Auth.Command.SignUp
{
    public class SignUpCommand : IRequest<Results<AuthResult>>
    {
        public string? Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
