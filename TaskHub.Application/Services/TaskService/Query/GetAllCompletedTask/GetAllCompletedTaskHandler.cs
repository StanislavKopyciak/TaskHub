using AutoMapper;
using MediatR;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

namespace TaskHub.Application.Services.TaskService.Query.GetAllCompletedTask
{
    public class GetAllCompletedTaskHandler
        : IRequestHandler<GetAllCompletedTaskQuery, IEnumerable<TaskItemDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository<TaskItem> _taskRepository;

        public GetAllCompletedTaskHandler(IMapper mapper, ITaskRepository<TaskItem> taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskItemDTO>> Handle(GetAllCompletedTaskQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllByUserIdAndStateAsync(request.UserId, request.State);

            return _mapper.Map<IEnumerable<TaskItemDTO>>(tasks);
        }
    }
}
