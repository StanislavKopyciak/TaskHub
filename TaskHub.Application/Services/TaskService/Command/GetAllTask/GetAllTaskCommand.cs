using MediatR;
using TaskHub.Application.DTO.TaskItem;

namespace TaskHub.Application.Services.TaskService.Command.GetAllTask
{
    public class GetAllCompletedTasksCommand : IRequest<IEnumerable<TaskItemDTO>>
    {
        public Guid UserId { get; set; }
    }
}
