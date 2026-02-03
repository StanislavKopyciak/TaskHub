using MediatR;

namespace TaskHub.Application.Services.TaskService.Command.NotCompleteTask
{
    public record ResetTaskStateCommand(Guid TaskId, Guid UserId) : IRequest<bool>;

}
