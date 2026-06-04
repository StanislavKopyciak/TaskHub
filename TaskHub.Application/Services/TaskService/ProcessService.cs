using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;
using TaskHub.Core.Enums;

namespace TaskHub.Application.Services.TaskService
{
    public class ProcessService
    {
        private readonly ITaskRepository _taskRepository;

        public ProcessService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task UpdateTaskStateAsync(Guid userId, CancellationToken ct)
        {
            var tasks = await _taskRepository.GetAllByUserIdAsync(userId, ct);

            var activeTasks = tasks
                .Where(t => t.State != State.Completed && t.State != State.NotCompleted);

            foreach (var task in activeTasks)
            {
                if (task.DeadLine == default)
                    continue;

                if (task.DeadLine <= DateTime.Now)
                {
                    task.State = State.NotCompleted;
                    await _taskRepository.UpdateAsync(task, ct);
                }
            }
        }

    }
}
