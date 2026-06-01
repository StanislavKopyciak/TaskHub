using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;
using TaskHub.Core.Enums;

namespace TaskHub.Application.Services.TaskService
{
    public class ProcessService
    {
        private readonly ITaskRepository<TaskItem> _taskRepository;

        public ProcessService(ITaskRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task UpdateTaskStateAsync(Guid userId)
        {
            var tasks = await _taskRepository.GetAllByUserIdAsync(userId);

            var activeTasks = tasks
                .Where(t => t.State != State.Completed && t.State != State.NotCompleted);

            foreach (var task in activeTasks)
            {
                if (task.DeadLine == default)
                    continue;

                if (task.DeadLine <= DateTime.Now)
                {
                    task.State = State.NotCompleted;
                    await _taskRepository.UpdateAsync(task.Id, task);
                }
            }
        }

    }
}
