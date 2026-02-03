using TaskHub.Application.DTO.TaskItem;
using TaskHub.Core.Entities;

namespace TaskHub.Application.Services.TaskService
{
    public interface ITaskService
    {
        Task<TaskItemDTO> GetByIdAsync(Guid id);
        Task<TaskItemDTO> AddAsync(Guid id, TaskCreateDTO item);
        Task<int> UpdateAsync(Guid id, TaskCreateDTO item);
        Task<int> DeleteAsync(Guid id);
        Task<IEnumerable<TaskItemDTO>> GetAllByUserIdAsync(Guid userId);
    }
}
