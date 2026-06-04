using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskHub.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Core.Entities.User>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasMany(u => u.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            builder.Property(u => u.EmailVerified)
                .IsRequired()
                .HasDefaultValue(false);    

            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
