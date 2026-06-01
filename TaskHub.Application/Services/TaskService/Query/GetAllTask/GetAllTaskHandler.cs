using AutoMapper;
using MediatR;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

namespace TaskHub.Application.Services.TaskService.Query.GetAllTask
{
    public class GetAllTasksHandler : IRequestHandler<GetAllCompletedTasksCommand, IEnumerable<TaskItemDTO>>
    {
        private readonly ITaskRepository<TaskItem> _taskRepository;
        private readonly IMapper _mapper;

        public GetAllTasksHandler(ITaskRepository<TaskItem> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskItemDTO>> Handle(GetAllCompletedTasksCommand request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllByUserIdAsync(request.UserId);

            return _mapper.Map<IEnumerable<TaskItemDTO>>(tasks);
        }
    }
}
