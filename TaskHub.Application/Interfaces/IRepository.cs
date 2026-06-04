using TaskHub.Core.Entities;

namespace TaskHub.Application.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(Guid id, CancellationToken ct);
        Task<T> AddAsync(T task, CancellationToken ct);
        Task<int> UpdateAsync(T task, CancellationToken ct);
        Task<int> DeleteAsync(Guid id, CancellationToken ct);
    }
}
