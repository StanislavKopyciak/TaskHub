using MediatR;
using TaskHub.Application.DTO.TaskItem;

namespace TaskHub.Application.Services.TaskService.Query.GetAllTasks
{
    public class GetAllTasksQuery : IRequest<IEnumerable<TaskItemDTO>>
    {
        public Guid UserId { get; set; }
    }
}
