using MediatR;
using TaskHub.Application.Interfaces;
using TaskHub.Application.Services.TaskService.Command.CompleteTask;
using TaskHub.Core.Entities;
using TaskHub.Core.Enums;


public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand, bool>
{
    private readonly ITaskRepository<TaskItem> _taskRepository;

    public CompleteTaskCommandHandler(ITaskRepository<TaskItem> taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<bool> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId);

        if (task == null)
            return false;

        if (task.UserId != request.UserId)
            return false;

        task.State = State.Completed;

        await _taskRepository.UpdateAsync(task.Id, task);

        return true;
    }
}