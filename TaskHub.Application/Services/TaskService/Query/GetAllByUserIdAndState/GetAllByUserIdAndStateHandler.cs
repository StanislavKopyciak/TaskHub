using AutoMapper;
using MediatR;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.Interfaces;

namespace TaskHub.Application.Services.TaskService.Query.GetAllByUserIdAndState
{
    public class GetAllByUserIdAndStateHandler : IRequestHandler<GetAllByUserIdAndStateQuery, IEnumerable<TaskItemDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public GetAllByUserIdAndStateHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskItemDTO>> Handle(GetAllByUserIdAndStateQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllByUserIdAndStateAsync(request.UserId, request.State, cancellationToken);

            return _mapper.Map<IEnumerable<TaskItemDTO>>(tasks);
        }
    }
}
