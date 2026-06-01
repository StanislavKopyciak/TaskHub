using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Core.Enums;

namespace TaskHub.Application.Services.TaskService.Query.GetAllCompletedTask
{
    public class GetAllCompletedTaskQuery : IRequest<IEnumerable<TaskItemDTO>>
    {
        public Guid UserId { get; set; }
        public State State { get; set; }
    }
}
