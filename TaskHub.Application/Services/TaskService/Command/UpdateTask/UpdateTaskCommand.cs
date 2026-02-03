using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Core.Enums;

namespace TaskHub.Application.Services.TaskService.Command.UpdateTask
{
    public class UpdateTaskCommand : IRequest<Results<TaskItemDTO>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
        public float HowMuchTime { get; set; }
        public Priority Priority { get; set; }
    }
}
