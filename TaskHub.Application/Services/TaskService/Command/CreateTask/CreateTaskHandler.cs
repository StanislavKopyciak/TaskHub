using AutoMapper;
using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Core.Entities;
using TaskHub.Core.Enums;
using TaskHub.Core.Interfaces;

namespace TaskHub.Application.Services.TaskService.Command.CreateTask
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Results<TaskItemDTO>>
    {
        private readonly ITaskRepository<TaskItem> _taskRepository;
        private readonly IMapper _mapper;
        public CreateTaskHandler(ITaskRepository<TaskItem> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<Results<TaskItemDTO>> Handle(CreateTaskCommand command, CancellationToken ct)
        {
            var task = new TaskItem
            {
                UserId = command.UserId,
                Title = command.Title,
                Description = command.Description ?? string.Empty,
                DeadLine = command.DeadLine == default ? DateTime.Now.AddDays(1) : command.DeadLine,
                Priority = command.Priority == default ? Priority.None : command.Priority
            };

            if (task.UserId != command.UserId)
                return Results<TaskItemDTO>.Fail("UserId mismatch.");

            var createdTask = await _taskRepository.AddAsync(task.UserId, task);

            return Results<TaskItemDTO>.Ok(_mapper.Map<TaskItemDTO>(createdTask));
        }
    }
}
