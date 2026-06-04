using MediatR;
using TaskHub.Application.Common;

namespace TaskHub.Application.Services.UserService.Auth.Command.VerifyEmail
{
    public class VerifyEmailCommand : IRequest<Results<AuthResult>>
    {
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
