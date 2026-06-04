using AutoMapper;
using MediatR;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

namespace TaskHub.Application.Services.TaskService.Query.GetAllTasks
{
    public class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskItemDTO>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public GetAllTasksHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskItemDTO>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllByUserIdAsync(request.UserId, cancellationToken);

            return _mapper.Map<IEnumerable<TaskItemDTO>>(tasks);
        }
    }
}
