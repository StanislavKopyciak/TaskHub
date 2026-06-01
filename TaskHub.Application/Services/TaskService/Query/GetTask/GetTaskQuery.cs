using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;

namespace TaskHub.Application.Services.TaskService.Query.GetTask
{
    public class GetTaskQuery : IRequest<Results<TaskItemDTO>>
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
    }
}
