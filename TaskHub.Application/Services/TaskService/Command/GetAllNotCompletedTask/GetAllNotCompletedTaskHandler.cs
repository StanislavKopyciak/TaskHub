using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.Services.TaskService.Command.GetAllCompletedTask;
using TaskHub.Application.Services.TaskService.Command.GetAllTask;
using TaskHub.Core.Entities;
using TaskHub.Core.Interfaces;

namespace TaskHub.Application.Services.TaskService.Command.GetAllNotCompletedTask
{
    public class GetAllNotCompletedTaskHandler
        : IRequestHandler<GetAllNotCompletedTaskCommand, IEnumerable<TaskItemDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository<TaskItem> _taskRepository;

        public GetAllNotCompletedTaskHandler(IMapper mapper, ITaskRepository<TaskItem> taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskItemDTO>> Handle(GetAllNotCompletedTaskCommand request, CancellationToken cancellationToken)
        {

            var tasks = await _taskRepository.GetAllByUserIdAndStateAsync(request.UserId, request.State);

            return _mapper.Map<IEnumerable<TaskItemDTO>>(tasks);
        }
    }
}
