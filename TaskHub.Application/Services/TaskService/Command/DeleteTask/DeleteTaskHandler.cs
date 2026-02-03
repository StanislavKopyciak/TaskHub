using AutoMapper;
using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Core.Entities;
using TaskHub.Core.Interfaces;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, Results<TaskItemDTO>>
{
    private readonly ITaskRepository<TaskItem> _taskRepository;
    private readonly IMapper _mapper;

    public DeleteTaskHandler(ITaskRepository<TaskItem> taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<Results<TaskItemDTO>> Handle(DeleteTaskCommand command, CancellationToken ct)
    {
        var task = await _taskRepository.GetByIdAsync(command.Id);

        if (task == null)
            return Results<TaskItemDTO>.Fail("Завдання не знайдено.");

        if (task.UserId != command.UserId)
            return Results<TaskItemDTO>.Fail("Немає прав на видалення цього завдання.");

        int deleted = await _taskRepository.DeleteAsync(command.Id);

        if (deleted == 0)
            return Results<TaskItemDTO>.Fail("Не вдалося видалити завдання.");

        return Results<TaskItemDTO>.Ok(_mapper.Map<TaskItemDTO>(task));
    }
}
