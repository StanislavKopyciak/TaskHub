using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.User;

namespace TaskHub.Application.Services.UserService.Auth.Command.SignIn
{
    // SignInCommand should represent a request, not a handler.
    public class SignInCommand : IRequest<Results<UserDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
