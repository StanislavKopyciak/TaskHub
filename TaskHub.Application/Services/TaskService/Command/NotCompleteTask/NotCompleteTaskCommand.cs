using MediatR;

namespace TaskHub.Application.Services.TaskService.Command.NotCompleteTask
{
    public record NotCompleteTaskCommand(Guid TaskId, Guid UserId) : IRequest<bool>;

}
