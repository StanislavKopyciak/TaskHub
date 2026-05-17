using AutoMapper;
using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.DTO.User;
using TaskHub.Core.Entities;
using TaskHub.Core.Interfaces;

namespace TaskHub.Application.Services.TaskService.Command.UpdateTask
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, Results<TaskItemDTO>>
    {
        private readonly ITaskRepository<TaskItem> _taskRepository;
        private readonly IMapper _mapper;
        public UpdateTaskHandler(ITaskRepository<TaskItem> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<Results<TaskItemDTO>> Handle(UpdateTaskCommand command, CancellationToken ct)
        {
            var task = await _taskRepository.GetByIdAsync(command.Id);

            if (task.UserId != command.UserId)
            {
                return Results<TaskItemDTO>.Fail("Айді не співпадають");
            }

                if (task == null)
            {
                return Results<TaskItemDTO>.Fail("Завдання не знайдено.");
            }

            if (task.UserId != command.UserId)
            {
                return Results<TaskItemDTO>.Fail("Ви не маєте дозволу оновлювати це завдання.");
            }


            task.Title = command.Title;
            task.Description = command.Description;
            task.DeadLine = command.DeadLine;
            task.Priority = command.Priority;
            task.UpdatedAt = DateTime.Now;

            await _taskRepository.UpdateAsync(command.Id, task);

            return Results<TaskItemDTO>.Ok(_mapper.Map<TaskItemDTO>(task));
        }
    }
}
