using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Core.Entities;
using TaskHub.Core.Interfaces;

namespace TaskHub.Application.Services.TaskService.Commands.GetTask
{
    public class GetTaskHandler : IRequestHandler<GetTaskCommand, Results<TaskItemDTO>>
    {
        private readonly ITaskRepository<TaskItem> _taskRepository;
        private readonly IMapper _mapper;

        public GetTaskHandler(
            ITaskRepository<TaskItem> taskRepository,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<Results<TaskItemDTO>> Handle(GetTaskCommand command, CancellationToken ct)
        {
            var task = await _taskRepository.GetByIdAsync(command.TaskId);

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
