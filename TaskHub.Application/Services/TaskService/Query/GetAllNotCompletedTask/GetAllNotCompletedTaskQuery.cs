using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Core.Enums;

namespace TaskHub.Application.Services.TaskService.Query.GetAllNotCompletedTask
{
    public class GetAllNotCompletedTaskQuery : IRequest<IEnumerable<TaskItemDTO>>
    {
        public Guid UserId { get; set; }
        public State State { get; set; } = State.Completed;
    }
}
