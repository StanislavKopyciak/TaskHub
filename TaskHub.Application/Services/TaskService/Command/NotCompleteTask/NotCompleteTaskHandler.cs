using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;
using TaskHub.Core.Enums;

namespace TaskHub.Application.Services.TaskService.Command.NotCompleteTask
{
    public class NotCompleteTaskHandler
    {
        private readonly ITaskRepository _taskRepository;

        public NotCompleteTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> Handle(NotCompleteTaskCommand request, Results<TaskItemDTO> results, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.TaskId, cancellationToken);
            if (task == null)
                return false;

            if (task.UserId != request.UserId)
            {
                return false;
            }

            task.State = State.NotCompleted;
            await _taskRepository.UpdateAsync(task, cancellationToken);

            return true;
        }
    }
}
