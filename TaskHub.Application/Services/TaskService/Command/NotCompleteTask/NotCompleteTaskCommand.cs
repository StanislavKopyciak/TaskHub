using MediatR;

namespace TaskHub.Application.Services.TaskService.Command.NotCompleteTask
{
    public record NotCompleteTaskCommand : IRequest<bool>
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
    }

}
