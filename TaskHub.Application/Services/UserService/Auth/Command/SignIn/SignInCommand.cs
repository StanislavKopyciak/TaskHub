using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.User;

namespace TaskHub.Application.Services.UserService.Auth.Command.SignIn
{
    public class SignInCommand : IRequest<Results<AuthResult>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
