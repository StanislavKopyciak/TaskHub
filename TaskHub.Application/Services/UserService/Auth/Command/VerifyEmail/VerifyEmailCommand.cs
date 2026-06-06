using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.User;

namespace TaskHub.Application.Services.UserService.Auth.Command.VerifyEmail
{
    public class VerifyEmailCommand : IRequest<Results<AuthResult>>
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
    }
}
