using MediatR;
using TaskHub.Application.Common;

namespace TaskHub.Application.Services.UserService.Auth.Command.ResendCode
{
    public class ResendCodeCommand : IRequest<Results<bool>>
    {
        public Guid UserId { get; set; }
    }
}
