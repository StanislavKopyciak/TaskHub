using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Core.Enums;

namespace TaskHub.Application.Services.TaskService.Query.GetAllByUserIdAndState
{
    public class GetAllByUserIdAndStateQuery : IRequest<IEnumerable<TaskItemDTO>>
    {
        public Guid UserId { get; set; }
        public State State { get; set; }
    }
}
