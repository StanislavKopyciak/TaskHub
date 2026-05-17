using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Core.Entities;
using TaskHub.Core.Enums;
using TaskHub.Core.Interfaces;

namespace TaskHub.Application.Services.TaskService.Command.GetAllCompletedTask
{
    public class GetAllCompletedTaskHandler
        : IRequestHandler<GetAllCompletedTaskCommand, IEnumerable<TaskItemDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository<TaskItem> _taskRepository;

        public GetAllCompletedTaskHandler(IMapper mapper, ITaskRepository<TaskItem> taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskItemDTO>> Handle(GetAllCompletedTaskCommand request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllByUserIdAndStateAsync(request.UserId, request.State);

            return _mapper.Map<IEnumerable<TaskItemDTO>>(tasks);
        }
    }
}
