using Microsoft.EntityFrameworkCore;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;
using TaskHub.Core.Enums;


namespace TaskHub.Infrastructure.Data.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskHubContext _context;

        public TaskRepository(TaskHubContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllByUserIdAndStateAsync(Guid userId, State state, CancellationToken ct)
        {
            return await _context.Tasks
                .Where(x => x.UserId == userId && x.State == state)
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<TaskItem>> GetAllByUserIdAsync(Guid userId, CancellationToken ct)
        {
            return await _context.Tasks
                .Where(n => n.UserId == userId)
                .ToListAsync(ct);
        }

        public async Task<TaskItem> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(u => u.Id == id, ct);

            return task;
        }

        public async Task<TaskItem> AddAsync(TaskItem item, CancellationToken ct)
        {
            item.UpdatedAt = DateTime.Now;

            await _context.Tasks.AddAsync(item, ct);
            await _context.SaveChangesAsync(ct);
            return item;
        }

        public async Task<int> UpdateAsync(TaskItem item, CancellationToken ct)
        {
            return await _context.Tasks
                .Where(t => t.Id == item.Id)
                .ExecuteUpdateAsync(t => t
                    .SetProperty(t => t.Title, item.Title)
                    .SetProperty(t => t.Description, item.Description)
                    .SetProperty(t => t.State, item.State)
                    .SetProperty(t => t.UpdatedAt, DateTime.Now)
                    .SetProperty(t => t.Priority, item.Priority)
                    .SetProperty(t => t.DeadLine, item.DeadLine),
                    ct
                );
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken ct)
        {
            return await _context.Tasks
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync(ct);
        }
    }
}
