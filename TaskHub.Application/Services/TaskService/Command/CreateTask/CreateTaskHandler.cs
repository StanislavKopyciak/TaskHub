using AutoMapper;
using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;
using TaskHub.Core.Enums;

namespace TaskHub.Application.Services.TaskService.Command.CreateTask
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Results<TaskItemDTO>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public CreateTaskHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<Results<TaskItemDTO>> Handle(CreateTaskCommand command, CancellationToken ct)
        {
            var task = _mapper.Map<TaskItem>(command);

            if (task.UserId != command.UserId)
                return Results<TaskItemDTO>.Fail("UserId mismatch.");

            var createdTask = await _taskRepository.AddAsync(task, ct);

            var taskDto = _mapper.Map<TaskItemDTO>(createdTask);

            return Results<TaskItemDTO>.Ok(taskDto);
        }
    }
}
