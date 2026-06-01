using TaskHub.Core.Entities;
using TaskHub.Core.Enums;

namespace TaskHub.Application.Interfaces
{
    public interface ITaskRepository<T> : IRepository<T> where T : TaskItem
    {
        Task<T> AddAsync(Guid userId, T task);
        Task<IEnumerable<T>> GetAllByUserIdAndStateAsync(Guid userId, State state);
        Task<IEnumerable<T>> GetAllByUserIdAsync(Guid userId);
    }
}
