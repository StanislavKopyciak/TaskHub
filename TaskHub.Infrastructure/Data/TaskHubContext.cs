using Microsoft.EntityFrameworkCore;
using TaskHub.Core.Entities;

namespace TaskHub.Infrastructure.Data
{
    public class TaskHubContext : DbContext
    {
        public TaskHubContext(DbContextOptions<TaskHubContext> options) : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskHubContext).Assembly);
        }
    }
}
