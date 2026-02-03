using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;

public class DeleteTaskCommand : IRequest<Results<TaskItemDTO>>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}
