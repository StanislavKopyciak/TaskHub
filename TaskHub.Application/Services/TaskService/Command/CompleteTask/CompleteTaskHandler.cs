using MediatR;
using TaskHub.Core.Entities;
using TaskHub.Core.Enums;
using TaskHub.Core.Interfaces;

namespace TaskHub.Application.Services.TaskService.Command.CompleteTask
{
    public class CompleteTaskCommandHandler : IRequestHandler<NotCompleteCommand, bool>
    {
        private readonly ITaskRepository<TaskItem> _taskRepository;

        public CompleteTaskCommandHandler(ITaskRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> Handle(NotCompleteCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.TaskId);
            if (task == null)
                return false;

            task.State = State.Completed;
            await _taskRepository.UpdateAsync(task.Id, task);

            return true;
        }
    }
}
