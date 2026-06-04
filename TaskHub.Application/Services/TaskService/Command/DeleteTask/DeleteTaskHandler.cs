using AutoMapper;
using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, Results<TaskItemDTO>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public DeleteTaskHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<Results<TaskItemDTO>> Handle(DeleteTaskCommand command, CancellationToken ct)
    {
        var task = await _taskRepository.GetByIdAsync(command.Id, ct);

        if (task.UserId != command.UserId)
            return Results<TaskItemDTO>.Fail("UserId mismatch.");

        if (task == null)
            return Results<TaskItemDTO>.Fail("Task not found.");

        if (task.UserId != command.UserId)
            return Results<TaskItemDTO>.Fail("Тo rights to delete the task.");

        int deleted = await _taskRepository.DeleteAsync(command.Id, ct);

        if (deleted == 0)
            return Results<TaskItemDTO>.Fail("Failed to delete task.");

        return Results<TaskItemDTO>.Ok(_mapper.Map<TaskItemDTO>(task));
    }
}
