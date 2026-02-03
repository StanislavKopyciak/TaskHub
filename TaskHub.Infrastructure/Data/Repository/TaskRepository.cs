using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TaskHub.Core.Entities;
using TaskHub.Core.Enums;
using TaskHub.Core.Interfaces;

namespace TaskHub.Infrastructure.Data.Repository
{
    public class TaskRepository : ITaskRepository<TaskItem>
    {
        private readonly TaskHubContext _context;

        public TaskRepository(TaskHubContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> AddAsync(Guid id, TaskItem item)
        {
            item.UserId = id;
            item.Id = Guid.NewGuid();
            item.CreatedAt = DateTime.Now;
            item.UpdatedAt = DateTime.Now;

            await _context.Tasks.AddAsync(item);   
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await _context.Tasks
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllByUserIdAsync(Guid userId)
        {
            var task = await _context.Tasks
                .Where(n => n.UserId == userId)
                .ToListAsync();
            return task;
        }

        public async Task<IEnumerable<TaskItem>> GetAllByUserIdAndStateAsync(Guid userId, State state)
        {
            return await _context.Tasks
                .Where(x => x.UserId == userId && x.State == state)
                .ToListAsync();
        }



        public async Task<TaskItem> GetByIdAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                throw new InvalidOperationException($"Task with id {id} not found.");
            return task;
        }

        public async Task<int> UpdateAsync(Guid id, TaskItem item)
        {
            return await _context.Tasks
                .Where(t => t.Id == id)
                .ExecuteUpdateAsync(t => t
                    .SetProperty(t => t.Title, item.Title)
                    .SetProperty(t => t.Description, item.Description)
                    .SetProperty(t => t.State, item.State)
                    .SetProperty(t => t.UpdatedAt, DateTime.Now)
                    .SetProperty(t => t.Priority, item.Priority)
                    .SetProperty(t => t.DeadLine, item.DeadLine)
                );
        }

        Task<TaskItem> IRepository<TaskItem>.AddAsync(TaskItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
