

namespace TaskHub.Application.Interfaces
{
    public interface IRepository<T> 
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<int> UpdateAsync(Guid id, T entity);
        Task<int> DeleteAsync(Guid id);
    }
}
