
using MediatR;

namespace TaskHub.Application.Services.TaskService.Command.CompleteTask
{
    public record NotCompleteCommand(Guid TaskId) : IRequest<bool>;
}
