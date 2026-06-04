using AutoMapper;
using MediatR;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

namespace TaskHub.Application.Services.TaskService.Query.GetTask
{
    public class GetTaskHandler : IRequestHandler<GetTaskQuery, Results<TaskItemDTO>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public GetTaskHandler(
            ITaskRepository repository,
            IMapper mapper)
        {
            _taskRepository = repository;
            _mapper = mapper;
        }

        public async Task<Results<TaskItemDTO>> Handle(GetTaskQuery command, CancellationToken ct)
        {
            var task = await _taskRepository.GetByIdAsync(command.TaskId, ct);

            if (task.UserId != command.UserId) {
                return Results<TaskItemDTO>.Fail("Айді не співпадають");
            }

            if (task == null)
            {
                return Results<TaskItemDTO>.Fail("Завдання не знайдено.");
            }

            if (task.UserId != command.UserId)
            {
                return Results<TaskItemDTO>.Fail("Немає доступу до цього завдання.");
            }

            var dto = _mapper.Map<TaskItemDTO>(task);

            return Results<TaskItemDTO>.Ok(dto);
        }
    }
}
