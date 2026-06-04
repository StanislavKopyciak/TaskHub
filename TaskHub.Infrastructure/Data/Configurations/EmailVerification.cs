
using Microsoft.EntityFrameworkCore;

namespace TaskHub.Infrastructure.Data.Configurations
{
    public class EmailVerification : IEntityTypeConfiguration<Core.Entities.EmailVerification>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Entities.EmailVerification> builder)
        {
            builder.ToTable("EmailVerifications");
            builder.Property(ev => ev.Code)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(ev => ev.Expiration)
                .IsRequired();
        }
    }
}
