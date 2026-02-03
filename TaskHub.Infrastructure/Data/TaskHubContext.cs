using Microsoft.EntityFrameworkCore;
using TaskHub.Core.Entities;

namespace TaskHub.Infrastructure.Data
{
    public class TaskHubContext : DbContext
    {
        public TaskHubContext(DbContextOptions<TaskHubContext> options) : base(options)
        {
        }

        public DbSet<Core.Entities.TaskItem> Tasks { get; set; }
        public DbSet<Core.Entities.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
