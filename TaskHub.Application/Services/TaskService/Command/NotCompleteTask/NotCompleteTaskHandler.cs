using TaskHub.Application.Common;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.Services.TaskService.Command.CompleteTask;
using TaskHub.Core.Entities;
using TaskHub.Core.Enums;
using TaskHub.Core.Interfaces;

namespace TaskHub.Application.Services.TaskService.Command.NotCompleteTask
{
    public class NotCompleteTaskHandler
    {
        private readonly ITaskRepository<TaskItem> _taskRepository;

        public NotCompleteTaskHandler(ITaskRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> Handle(NotCompleteTaskCommand request, Results<TaskItemDTO> results, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.TaskId);
            if (task == null)
                return false;

            if (task.UserId != request.UserId)
            {
                return false;
            }

                task.State = State.NotCompleted;
            await _taskRepository.UpdateAsync(task.Id, task);

            return true;
        }
    }
}
