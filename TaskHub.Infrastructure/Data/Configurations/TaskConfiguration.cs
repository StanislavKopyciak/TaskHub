using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskHub.Infrastructure.Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Core.Entities.TaskItem>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.TaskItem> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasMaxLength(1000);

            builder.Property(t => t.CreatedAt)
                .IsRequired();

            builder.Property(t => t.UpdatedAt)
                .IsRequired();

            builder.Property(t => t.State)
                .HasConversion<int>();

            builder.Property(t => t.DeadLine)
                .IsRequired();

            builder.Property(t => t.Priority)
                .HasConversion<int>();

            builder.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId);
        }
    }
}
