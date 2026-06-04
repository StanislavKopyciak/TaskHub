using TaskHub.Core.Entities;
using TaskHub.Core.Enums;

namespace TaskHub.Application.Interfaces
{
    public interface ITaskRepository : IRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetAllByUserIdAndStateAsync(Guid userId, State state, CancellationToken ct);
        Task<IEnumerable<TaskItem>> GetAllByUserIdAsync(Guid userId, CancellationToken ct);
    }
}
