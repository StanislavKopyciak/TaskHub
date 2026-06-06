
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskHub.Core.Entities;

namespace TaskHub.Infrastructure.Data.Configurations
{
    public class EmailVerificationConfiguration : IEntityTypeConfiguration<EmailVerification>
    {
        public void Configure(EntityTypeBuilder<EmailVerification> builder)
        {
            builder.ToTable("EmailVerifications");

            builder.HasKey(ev => ev.Id);

            builder.Property(ev => ev.UserId)
                .IsRequired();

            builder.Property(ev => ev.Code)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ev => ev.Expiration)
                .IsRequired();

            builder.Property(ev => ev.IsUsed)
                .IsRequired();

            builder.Property(ev => ev.CreatedAt)
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
